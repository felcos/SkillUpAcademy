using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.DTOs.IA;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de chat con la IA (Aria) usando el proveedor activo configurado en la BD.
/// Soporta Anthropic, OpenAI y proveedores compatibles con la API de OpenAI (Groq, Mistral).
/// </summary>
public class ServicioChatIA : IServicioChatIA
{
    private readonly IRepositorioChatIA _repositorio;
    private readonly IServicioSeguridadIA _seguridadIA;
    private readonly AppDbContext _contexto;
    private readonly ILogger<ServicioChatIA> _logger;
    private readonly HttpClient _httpClient;

    private const int MaxMensajesPorSesion = 50;

    private const string PromptSistemaAria = """
        Eres Aria, instructora virtual de SkillUp Academy. Tu especialidad es enseñar habilidades blandas profesionales.

        Reglas:
        - Responde SIEMPRE en español.
        - Sé motivadora, profesional y empática.
        - Usa ejemplos prácticos del mundo laboral real.
        - Si el usuario pregunta algo fuera de habilidades profesionales, redirige amablemente.
        - Máximo 300 palabras por respuesta.
        - Usa formato Markdown cuando ayude a la claridad.
        - Si es una sesión de roleplay, actúa como el personaje del escenario.
        - Si es una sesión de guía de lección, ayuda a profundizar en el tema.
        - Termina siempre con una pregunta o sugerencia para mantener la conversación activa.
        """;

    /// <summary>
    /// Constructor del servicio de chat IA.
    /// </summary>
    public ServicioChatIA(
        IRepositorioChatIA repositorio,
        IServicioSeguridadIA seguridadIA,
        AppDbContext contexto,
        ILogger<ServicioChatIA> logger,
        HttpClient httpClient)
    {
        _repositorio = repositorio;
        _seguridadIA = seguridadIA;
        _contexto = contexto;
        _logger = logger;
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<SesionIADto> IniciarSesionAsync(PeticionIniciarSesionIA peticion, Guid usuarioId)
    {
        if (!Enum.TryParse<TipoSesionIA>(peticion.TipoSesion, true, out TipoSesionIA tipoSesion))
        {
            throw new ExcepcionValidacion("TipoSesion no válido. Valores permitidos: GuiaLeccion, Roleplay, PracticaLibre.");
        }

        SesionChatIA sesion = new SesionChatIA
        {
            UsuarioId = usuarioId,
            LeccionId = peticion.LeccionId,
            TipoSesion = tipoSesion,
            FechaInicio = DateTime.UtcNow,
            Activa = true
        };

        await _repositorio.CrearSesionAsync(sesion);

        // Guardar el prompt del sistema como primer mensaje
        string promptSistema = ConstruirPromptSistema(tipoSesion, peticion.LeccionId);
        MensajeChatIA mensajeSistema = new MensajeChatIA
        {
            SesionId = sesion.Id,
            Rol = "system",
            Contenido = promptSistema,
            FechaEnvio = DateTime.UtcNow
        };
        await _repositorio.AgregarMensajeAsync(mensajeSistema);

        string mensajeBienvenida = GenerarMensajeBienvenida(tipoSesion);

        // Guardar mensaje de bienvenida como mensaje del asistente
        MensajeChatIA mensajeAsistente = new MensajeChatIA
        {
            SesionId = sesion.Id,
            Rol = "assistant",
            Contenido = mensajeBienvenida,
            FechaEnvio = DateTime.UtcNow
        };
        await _repositorio.AgregarMensajeAsync(mensajeAsistente);

        sesion.ContadorMensajes = 1;
        await _repositorio.ActualizarSesionAsync(sesion);

        _logger.LogInformation("Sesión de chat IA {SesionId} iniciada para usuario {UsuarioId}, tipo {TipoSesion}",
            sesion.Id, usuarioId, tipoSesion);

        return new SesionIADto
        {
            Id = sesion.Id,
            TipoSesion = tipoSesion.ToString(),
            LeccionId = peticion.LeccionId,
            FechaInicio = sesion.FechaInicio,
            MensajeInicial = mensajeBienvenida
        };
    }

    /// <inheritdoc />
    public async Task<RespuestaMensajeIADto> EnviarMensajeAsync(Guid sesionId, PeticionMensajeIA peticion, Guid usuarioId)
    {
        SesionChatIA? sesion = await _repositorio.ObtenerSesionConMensajesAsync(sesionId);
        if (sesion == null)
            throw new ExcepcionNoEncontrado("SesionChatIA", sesionId);

        if (sesion.UsuarioId != usuarioId)
            throw new ExcepcionNoAutorizado("No tienes acceso a esta sesión de chat.");

        if (!sesion.Activa)
            throw new ExcepcionValidacion("Esta sesión de chat ya fue cerrada.");

        int totalMensajes = await _repositorio.ContarMensajesEnSesionAsync(sesionId);
        if (totalMensajes >= MaxMensajesPorSesion)
        {
            throw new ExcepcionValidacion($"Se alcanzó el límite de {MaxMensajesPorSesion} mensajes por sesión. Inicia una nueva sesión.");
        }

        // Verificar si el usuario está bloqueado por abuso
        bool estaBloqueado = await _seguridadIA.UsuarioBloqueadoAsync(usuarioId);
        if (estaBloqueado)
        {
            throw new ExcepcionValidacion("Tu cuenta ha sido temporalmente restringida por uso indebido del chat. Contacta soporte si crees que es un error.");
        }

        // Validar entrada del usuario (5 capas de seguridad)
        ResultadoValidacionIA validacionEntrada = await _seguridadIA.ValidarEntradaAsync(peticion.Mensaje, usuarioId, sesionId);
        if (!validacionEntrada.EsSeguro)
        {
            _logger.LogWarning("Mensaje rechazado por seguridad IA: {Razon} - Usuario: {UsuarioId}",
                validacionEntrada.Razon, usuarioId);

            if (!string.IsNullOrWhiteSpace(validacionEntrada.CategoriaViolacion))
            {
                await _seguridadIA.RegistrarStrikeAsync(
                    usuarioId, sesionId,
                    validacionEntrada.CategoriaViolacion,
                    peticion.Mensaje,
                    "ValidarEntradaAsync");
            }

            return new RespuestaMensajeIADto
            {
                Respuesta = validacionEntrada.MensajeAlternativo
                    ?? "Lo siento, no puedo procesar ese mensaje. ¿Podrías reformularlo?",
                FueMarcado = true,
                TokensUsados = 0,
                Sugerencias = GenerarSugerencias(sesion.TipoSesion, peticion.Mensaje)
            };
        }

        // Guardar mensaje del usuario
        MensajeChatIA mensajeUsuario = new MensajeChatIA
        {
            SesionId = sesionId,
            Rol = "user",
            Contenido = peticion.Mensaje,
            FechaEnvio = DateTime.UtcNow
        };
        await _repositorio.AgregarMensajeAsync(mensajeUsuario);

        // Obtener historial para contexto
        IReadOnlyList<MensajeChatIA> historial = sesion.Mensajes.ToList();

        // Llamar a la API del proveedor activo
        (string respuestaTexto, int tokensUsados) = await LlamarApiAsync(historial, peticion.Mensaje);

        // Validar salida de la IA (filtro de datos personales, truncado)
        ResultadoValidacionIA validacionSalida = await _seguridadIA.ValidarSalidaAsync(respuestaTexto);
        if (!validacionSalida.EsSeguro && !string.IsNullOrWhiteSpace(validacionSalida.MensajeAlternativo))
        {
            _logger.LogWarning("Respuesta de IA filtrada por seguridad: {Razon}", validacionSalida.Razon);
            respuestaTexto = validacionSalida.MensajeAlternativo;
        }

        // Guardar respuesta del asistente
        MensajeChatIA mensajeAsistente = new MensajeChatIA
        {
            SesionId = sesionId,
            Rol = "assistant",
            Contenido = respuestaTexto,
            TokensUsados = tokensUsados,
            FechaEnvio = DateTime.UtcNow
        };
        await _repositorio.AgregarMensajeAsync(mensajeAsistente);

        // Actualizar contador
        sesion.ContadorMensajes += 2;
        await _repositorio.ActualizarSesionAsync(sesion);

        // Generar sugerencias
        List<string> sugerencias = GenerarSugerencias(sesion.TipoSesion, peticion.Mensaje);

        return new RespuestaMensajeIADto
        {
            Respuesta = respuestaTexto,
            FueMarcado = !validacionSalida.EsSeguro,
            TokensUsados = tokensUsados,
            Sugerencias = sugerencias
        };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<MensajeIADto>> ObtenerHistorialAsync(Guid sesionId, Guid usuarioId)
    {
        SesionChatIA? sesion = await _repositorio.ObtenerSesionAsync(sesionId);
        if (sesion == null)
            throw new ExcepcionNoEncontrado("SesionChatIA", sesionId);

        if (sesion.UsuarioId != usuarioId)
            throw new ExcepcionNoAutorizado("No tienes acceso a esta sesión de chat.");

        IReadOnlyList<MensajeChatIA> mensajes = await _repositorio.ObtenerMensajesAsync(sesionId);

        List<MensajeIADto> resultado = new List<MensajeIADto>();
        foreach (MensajeChatIA mensaje in mensajes)
        {
            // No devolver mensajes del sistema al usuario
            if (mensaje.Rol == "system")
                continue;

            resultado.Add(new MensajeIADto
            {
                Id = mensaje.Id,
                Rol = mensaje.Rol,
                Contenido = mensaje.Contenido,
                FechaEnvio = mensaje.FechaEnvio
            });
        }

        return resultado;
    }

    /// <inheritdoc />
    public async Task CerrarSesionAsync(Guid sesionId, Guid usuarioId)
    {
        SesionChatIA? sesion = await _repositorio.ObtenerSesionAsync(sesionId);
        if (sesion == null)
            throw new ExcepcionNoEncontrado("SesionChatIA", sesionId);

        if (sesion.UsuarioId != usuarioId)
            throw new ExcepcionNoAutorizado("No tienes acceso a esta sesión de chat.");

        sesion.Activa = false;
        sesion.FechaFin = DateTime.UtcNow;
        await _repositorio.ActualizarSesionAsync(sesion);

        _logger.LogInformation("Sesión de chat IA {SesionId} cerrada por usuario {UsuarioId}",
            sesionId, usuarioId);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<string> EnviarMensajeStreamAsync(
        Guid sesionId, PeticionMensajeIA peticion, Guid usuarioId)
    {
        CancellationToken cancellationToken = CancellationToken.None;
        SesionChatIA? sesion = await _repositorio.ObtenerSesionConMensajesAsync(sesionId);
        if (sesion == null)
            throw new ExcepcionNoEncontrado("SesionChatIA", sesionId);

        if (sesion.UsuarioId != usuarioId)
            throw new ExcepcionNoAutorizado("No tienes acceso a esta sesión de chat.");

        if (!sesion.Activa)
            throw new ExcepcionValidacion("Esta sesión de chat ya fue cerrada.");

        int totalMensajes = await _repositorio.ContarMensajesEnSesionAsync(sesionId);
        if (totalMensajes >= MaxMensajesPorSesion)
            throw new ExcepcionValidacion($"Se alcanzó el límite de {MaxMensajesPorSesion} mensajes por sesión. Inicia una nueva sesión.");

        bool estaBloqueado = await _seguridadIA.UsuarioBloqueadoAsync(usuarioId);
        if (estaBloqueado)
            throw new ExcepcionValidacion("Tu cuenta ha sido temporalmente restringida por uso indebido del chat. Contacta soporte si crees que es un error.");

        ResultadoValidacionIA validacionEntrada = await _seguridadIA.ValidarEntradaAsync(peticion.Mensaje, usuarioId, sesionId);
        if (!validacionEntrada.EsSeguro)
        {
            _logger.LogWarning("Mensaje rechazado por seguridad IA: {Razon} - Usuario: {UsuarioId}",
                validacionEntrada.Razon, usuarioId);

            if (!string.IsNullOrWhiteSpace(validacionEntrada.CategoriaViolacion))
            {
                await _seguridadIA.RegistrarStrikeAsync(
                    usuarioId, sesionId,
                    validacionEntrada.CategoriaViolacion,
                    peticion.Mensaje,
                    "ValidarEntradaAsync");
            }

            string mensajeRechazo = validacionEntrada.MensajeAlternativo
                ?? "Lo siento, no puedo procesar ese mensaje. ¿Podrías reformularlo?";
            yield return $"data: {JsonSerializer.Serialize(new { tipo = "texto", contenido = mensajeRechazo })}\n\n";
            yield return $"data: {JsonSerializer.Serialize(new { tipo = "fin", fueMarcado = true, tokensUsados = 0, sugerencias = GenerarSugerencias(sesion.TipoSesion, peticion.Mensaje) })}\n\n";
            yield break;
        }

        // Guardar mensaje del usuario
        MensajeChatIA mensajeUsuario = new MensajeChatIA
        {
            SesionId = sesionId,
            Rol = "user",
            Contenido = peticion.Mensaje,
            FechaEnvio = DateTime.UtcNow
        };
        await _repositorio.AgregarMensajeAsync(mensajeUsuario);

        IReadOnlyList<MensajeChatIA> historial = sesion.Mensajes.ToList();

        // Streaming de la respuesta
        StringBuilder respuestaCompleta = new StringBuilder();
        int tokensUsados = 0;

        await foreach (string fragmento in LlamarApiStreamAsync(historial, peticion.Mensaje, cancellationToken))
        {
            if (fragmento.StartsWith("{\"tokens\":"))
            {
                // Evento especial de tokens al final
                using JsonDocument doc = JsonDocument.Parse(fragmento);
                tokensUsados = doc.RootElement.GetProperty("tokens").GetInt32();
                continue;
            }

            respuestaCompleta.Append(fragmento);
            yield return $"data: {JsonSerializer.Serialize(new { tipo = "texto", contenido = fragmento })}\n\n";
        }

        string respuestaTexto = respuestaCompleta.ToString();

        // Validar salida
        ResultadoValidacionIA validacionSalida = await _seguridadIA.ValidarSalidaAsync(respuestaTexto);
        bool fueMarcado = !validacionSalida.EsSeguro;

        if (fueMarcado && !string.IsNullOrWhiteSpace(validacionSalida.MensajeAlternativo))
        {
            _logger.LogWarning("Respuesta de IA filtrada por seguridad: {Razon}", validacionSalida.Razon);
            respuestaTexto = validacionSalida.MensajeAlternativo;
            // Enviar reemplazo completo
            yield return $"data: {JsonSerializer.Serialize(new { tipo = "reemplazo", contenido = respuestaTexto })}\n\n";
        }

        // Guardar respuesta del asistente
        MensajeChatIA mensajeAsistente = new MensajeChatIA
        {
            SesionId = sesionId,
            Rol = "assistant",
            Contenido = respuestaTexto,
            TokensUsados = tokensUsados,
            FechaEnvio = DateTime.UtcNow
        };
        await _repositorio.AgregarMensajeAsync(mensajeAsistente);

        sesion.ContadorMensajes += 2;
        await _repositorio.ActualizarSesionAsync(sesion);

        List<string> sugerencias = GenerarSugerencias(sesion.TipoSesion, peticion.Mensaje);

        yield return $"data: {JsonSerializer.Serialize(new { tipo = "fin", fueMarcado, tokensUsados, sugerencias })}\n\n";
    }

    // ======================== OBTENER PROVEEDOR ACTIVO ========================

    /// <summary>
    /// Obtiene el proveedor de IA activo y habilitado con API key configurada.
    /// </summary>
    private async Task<ProveedorIA?> ObtenerProveedorActivoAsync()
    {
        ProveedorIA? proveedor = await _contexto.ProveedoresIA
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.EsActivo && p.Habilitado && p.ApiKey != null);

        if (proveedor == null || string.IsNullOrWhiteSpace(proveedor.ApiKey))
        {
            _logger.LogWarning("No hay proveedor de IA activo con API key configurada.");
            return null;
        }

        return proveedor;
    }

    /// <summary>
    /// Determina si un proveedor usa formato de API compatible con OpenAI.
    /// OpenAI, Groq y Mistral usan el mismo formato de API.
    /// </summary>
    private static bool EsFormatoOpenAI(TipoProveedorIA tipo)
    {
        return tipo is TipoProveedorIA.OpenAI or TipoProveedorIA.Groq or TipoProveedorIA.Mistral;
    }

    // ======================== LLAMADA SIN STREAM ========================

    /// <summary>
    /// Llama a la API del proveedor activo sin streaming.
    /// </summary>
    private async Task<(string Respuesta, int TokensUsados)> LlamarApiAsync(
        IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo)
    {
        ProveedorIA? proveedor = await ObtenerProveedorActivoAsync();
        if (proveedor == null)
            return (GenerarRespuestaFallback(), 0);

        try
        {
            if (EsFormatoOpenAI(proveedor.Tipo))
                return await LlamarApiOpenAIAsync(proveedor, historial, mensajeNuevo);

            return await LlamarApiAnthropicAsync(proveedor, historial, mensajeNuevo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al llamar a la API de {Proveedor}", proveedor.Tipo);
            return (GenerarRespuestaFallback(), 0);
        }
    }

    // ======================== LLAMADA CON STREAM ========================

    /// <summary>
    /// Llama a la API del proveedor activo con streaming.
    /// </summary>
    private async IAsyncEnumerable<string> LlamarApiStreamAsync(
        IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ProveedorIA? proveedor = await ObtenerProveedorActivoAsync();
        if (proveedor == null)
        {
            yield return GenerarRespuestaFallback();
            yield return "{\"tokens\":0}";
            yield break;
        }

        IAsyncEnumerable<string> stream = EsFormatoOpenAI(proveedor.Tipo)
            ? LlamarApiOpenAIStreamAsync(proveedor, historial, mensajeNuevo, cancellationToken)
            : LlamarApiAnthropicStreamAsync(proveedor, historial, mensajeNuevo, cancellationToken);

        await foreach (string fragmento in stream.WithCancellation(cancellationToken))
        {
            yield return fragmento;
        }
    }

    // ======================== ANTHROPIC ========================

    /// <summary>
    /// Llama a la API de Anthropic sin streaming.
    /// </summary>
    private async Task<(string Respuesta, int TokensUsados)> LlamarApiAnthropicAsync(
        ProveedorIA proveedor, IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo)
    {
        string url = proveedor.UrlBase.TrimEnd('/') + "/messages";

        string promptSistema = historial
            .FirstOrDefault(m => m.Rol == "system")?.Contenido ?? PromptSistemaAria;

        List<object> mensajesApi = new List<object>();
        foreach (MensajeChatIA mensaje in historial.Where(m => m.Rol != "system"))
        {
            mensajesApi.Add(new { role = mensaje.Rol, content = mensaje.Contenido });
        }
        mensajesApi.Add(new { role = "user", content = mensajeNuevo });

        object payload = new
        {
            model = proveedor.ModeloChat,
            max_tokens = proveedor.MaxTokens,
            temperature = proveedor.Temperatura,
            system = promptSistema,
            messages = mensajesApi
        };

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };
        solicitud.Headers.Add("x-api-key", proveedor.ApiKey);
        solicitud.Headers.Add("anthropic-version", "2023-06-01");

        HttpResponseMessage respuesta = await _httpClient.SendAsync(solicitud);
        string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();

        if (!respuesta.IsSuccessStatusCode)
        {
            _logger.LogError("Error de la API de Anthropic: {StatusCode} - {Body}",
                respuesta.StatusCode, jsonRespuesta);
            return (GenerarRespuestaFallback(), 0);
        }

        using JsonDocument documento = JsonDocument.Parse(jsonRespuesta);
        JsonElement raiz = documento.RootElement;

        string textoRespuesta = raiz
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString() ?? "Lo siento, no pude generar una respuesta.";

        int tokensEntrada = raiz.GetProperty("usage").GetProperty("input_tokens").GetInt32();
        int tokensSalida = raiz.GetProperty("usage").GetProperty("output_tokens").GetInt32();

        return (textoRespuesta, tokensEntrada + tokensSalida);
    }

    /// <summary>
    /// Llama a la API de Anthropic con streaming.
    /// </summary>
    private async IAsyncEnumerable<string> LlamarApiAnthropicStreamAsync(
        ProveedorIA proveedor, IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        string url = proveedor.UrlBase.TrimEnd('/') + "/messages";

        string promptSistema = historial
            .FirstOrDefault(m => m.Rol == "system")?.Contenido ?? PromptSistemaAria;

        List<object> mensajesApi = new List<object>();
        foreach (MensajeChatIA mensaje in historial.Where(m => m.Rol != "system"))
        {
            mensajesApi.Add(new { role = mensaje.Rol, content = mensaje.Contenido });
        }
        mensajesApi.Add(new { role = "user", content = mensajeNuevo });

        object payload = new
        {
            model = proveedor.ModeloChat,
            max_tokens = proveedor.MaxTokens,
            temperature = proveedor.Temperatura,
            system = promptSistema,
            messages = mensajesApi,
            stream = true
        };

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };
        solicitud.Headers.Add("x-api-key", proveedor.ApiKey);
        solicitud.Headers.Add("anthropic-version", "2023-06-01");

        HttpResponseMessage? respuesta = null;
        bool errorConexion = false;

        try
        {
            respuesta = await _httpClient.SendAsync(solicitud, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al conectar con la API de Anthropic (stream)");
            errorConexion = true;
        }

        if (errorConexion)
        {
            yield return GenerarRespuestaFallback();
            yield return "{\"tokens\":0}";
            yield break;
        }

        if (!respuesta!.IsSuccessStatusCode)
        {
            string cuerpoError = await respuesta.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Error de la API de Anthropic (stream): {StatusCode} - {Body}",
                respuesta.StatusCode, cuerpoError);
            yield return GenerarRespuestaFallback();
            yield return "{\"tokens\":0}";
            yield break;
        }

        using Stream stream = await respuesta.Content.ReadAsStreamAsync(cancellationToken);
        using StreamReader lector = new StreamReader(stream);

        int tokensTotal = 0;

        while (!lector.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string? linea = await lector.ReadLineAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(linea) || !linea.StartsWith("data: "))
                continue;

            string datos = linea.Substring(6);
            if (datos == "[DONE]")
                break;

            string? textoExtraido = ProcesarEventoStreamAnthropic(datos, ref tokensTotal);
            if (textoExtraido != null)
            {
                yield return textoExtraido;
            }
        }

        yield return $"{{\"tokens\":{tokensTotal}}}";
    }

    /// <summary>
    /// Procesa un evento SSE de la API de Anthropic y extrae texto si es un content_block_delta.
    /// </summary>
    private string? ProcesarEventoStreamAnthropic(string datosJson, ref int tokensTotal)
    {
        try
        {
            using JsonDocument doc = JsonDocument.Parse(datosJson);
            JsonElement raiz = doc.RootElement;
            string tipo = raiz.GetProperty("type").GetString() ?? "";

            if (tipo == "content_block_delta")
            {
                JsonElement delta = raiz.GetProperty("delta");
                string? texto = delta.GetProperty("text").GetString();
                return string.IsNullOrEmpty(texto) ? null : texto;
            }

            if (tipo == "message_delta")
            {
                if (raiz.TryGetProperty("usage", out JsonElement usage))
                {
                    tokensTotal += usage.GetProperty("output_tokens").GetInt32();
                }
            }
            else if (tipo == "message_start")
            {
                if (raiz.TryGetProperty("message", out JsonElement mensaje))
                {
                    if (mensaje.TryGetProperty("usage", out JsonElement usageInicio))
                    {
                        tokensTotal += usageInicio.GetProperty("input_tokens").GetInt32();
                    }
                }
            }
        }
        catch (JsonException)
        {
            // Ignorar líneas que no sean JSON válido
        }

        return null;
    }

    // ======================== OPENAI-COMPATIBLE (OpenAI, Groq, Mistral) ========================

    /// <summary>
    /// Llama a una API compatible con OpenAI sin streaming.
    /// </summary>
    private async Task<(string Respuesta, int TokensUsados)> LlamarApiOpenAIAsync(
        ProveedorIA proveedor, IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo)
    {
        string url = proveedor.UrlBase.TrimEnd('/') + "/chat/completions";

        List<object> mensajesApi = ConstruirMensajesOpenAI(historial, mensajeNuevo);

        object payload = new
        {
            model = proveedor.ModeloChat,
            max_tokens = proveedor.MaxTokens,
            temperature = proveedor.Temperatura,
            messages = mensajesApi
        };

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };
        solicitud.Headers.Authorization = new AuthenticationHeaderValue("Bearer", proveedor.ApiKey);

        HttpResponseMessage respuesta = await _httpClient.SendAsync(solicitud);
        string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();

        if (!respuesta.IsSuccessStatusCode)
        {
            _logger.LogError("Error de la API de {Proveedor}: {StatusCode} - {Body}",
                proveedor.Tipo, respuesta.StatusCode, jsonRespuesta);
            return (GenerarRespuestaFallback(), 0);
        }

        using JsonDocument documento = JsonDocument.Parse(jsonRespuesta);
        JsonElement raiz = documento.RootElement;

        string textoRespuesta = raiz
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "Lo siento, no pude generar una respuesta.";

        int tokensUsados = 0;
        if (raiz.TryGetProperty("usage", out JsonElement usage))
        {
            int tokensEntrada = usage.TryGetProperty("prompt_tokens", out JsonElement pt) ? pt.GetInt32() : 0;
            int tokensSalida = usage.TryGetProperty("completion_tokens", out JsonElement ct) ? ct.GetInt32() : 0;
            tokensUsados = tokensEntrada + tokensSalida;
        }

        return (textoRespuesta, tokensUsados);
    }

    /// <summary>
    /// Llama a una API compatible con OpenAI con streaming.
    /// </summary>
    private async IAsyncEnumerable<string> LlamarApiOpenAIStreamAsync(
        ProveedorIA proveedor, IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        string url = proveedor.UrlBase.TrimEnd('/') + "/chat/completions";

        List<object> mensajesApi = ConstruirMensajesOpenAI(historial, mensajeNuevo);

        object payload = new
        {
            model = proveedor.ModeloChat,
            max_tokens = proveedor.MaxTokens,
            temperature = proveedor.Temperatura,
            messages = mensajesApi,
            stream = true,
            stream_options = new { include_usage = true }
        };

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };
        solicitud.Headers.Authorization = new AuthenticationHeaderValue("Bearer", proveedor.ApiKey);

        HttpResponseMessage? respuesta = null;
        bool errorConexion = false;

        try
        {
            respuesta = await _httpClient.SendAsync(solicitud, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al conectar con la API de {Proveedor} (stream)", proveedor.Tipo);
            errorConexion = true;
        }

        if (errorConexion)
        {
            yield return GenerarRespuestaFallback();
            yield return "{\"tokens\":0}";
            yield break;
        }

        if (!respuesta!.IsSuccessStatusCode)
        {
            string cuerpoError = await respuesta.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Error de la API de {Proveedor} (stream): {StatusCode} - {Body}",
                proveedor.Tipo, respuesta.StatusCode, cuerpoError);
            yield return GenerarRespuestaFallback();
            yield return "{\"tokens\":0}";
            yield break;
        }

        using Stream stream = await respuesta.Content.ReadAsStreamAsync(cancellationToken);
        using StreamReader lector = new StreamReader(stream);

        int tokensTotal = 0;

        while (!lector.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string? linea = await lector.ReadLineAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(linea) || !linea.StartsWith("data: "))
                continue;

            string datos = linea.Substring(6);
            if (datos == "[DONE]")
                break;

            string? textoExtraido = ProcesarEventoStreamOpenAI(datos, ref tokensTotal);
            if (textoExtraido != null)
            {
                yield return textoExtraido;
            }
        }

        yield return $"{{\"tokens\":{tokensTotal}}}";
    }

    /// <summary>
    /// Construye el array de mensajes en formato OpenAI (system como primer mensaje del array).
    /// </summary>
    private List<object> ConstruirMensajesOpenAI(IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo)
    {
        string promptSistema = historial
            .FirstOrDefault(m => m.Rol == "system")?.Contenido ?? PromptSistemaAria;

        List<object> mensajesApi = new List<object>
        {
            new { role = "system", content = promptSistema }
        };

        foreach (MensajeChatIA mensaje in historial.Where(m => m.Rol != "system"))
        {
            mensajesApi.Add(new { role = mensaje.Rol, content = mensaje.Contenido });
        }
        mensajesApi.Add(new { role = "user", content = mensajeNuevo });

        return mensajesApi;
    }

    /// <summary>
    /// Procesa un evento SSE de una API compatible con OpenAI.
    /// </summary>
    private string? ProcesarEventoStreamOpenAI(string datosJson, ref int tokensTotal)
    {
        try
        {
            using JsonDocument doc = JsonDocument.Parse(datosJson);
            JsonElement raiz = doc.RootElement;

            // Extraer tokens si hay usage
            if (raiz.TryGetProperty("usage", out JsonElement usage) && usage.ValueKind != JsonValueKind.Null)
            {
                int tokensEntrada = usage.TryGetProperty("prompt_tokens", out JsonElement pt) ? pt.GetInt32() : 0;
                int tokensSalida = usage.TryGetProperty("completion_tokens", out JsonElement ct) ? ct.GetInt32() : 0;
                tokensTotal = tokensEntrada + tokensSalida;
            }

            // Extraer texto del delta
            if (raiz.TryGetProperty("choices", out JsonElement choices) && choices.GetArrayLength() > 0)
            {
                JsonElement primerChoice = choices[0];
                if (primerChoice.TryGetProperty("delta", out JsonElement delta))
                {
                    if (delta.TryGetProperty("content", out JsonElement content) && content.ValueKind == JsonValueKind.String)
                    {
                        string? texto = content.GetString();
                        return string.IsNullOrEmpty(texto) ? null : texto;
                    }
                }
            }
        }
        catch (JsonException)
        {
            // Ignorar líneas que no sean JSON válido
        }

        return null;
    }

    // ======================== HELPERS ========================

    private static string GenerarRespuestaFallback()
    {
        return "Lo siento, en este momento no tengo un proveedor de IA configurado. " +
               "Un administrador necesita activar y configurar un proveedor en el panel de administración. " +
               "Mientras tanto, te animo a explorar las lecciones y practicar los ejercicios.";
    }

    private static string ConstruirPromptSistema(TipoSesionIA tipoSesion, int? leccionId)
    {
        string promptBase = PromptSistemaAria;

        string contextoAdicional = tipoSesion switch
        {
            TipoSesionIA.RepasoDeLeccion => "\n\nContexto: El estudiante está en una sesión de repaso de lección. " +
                "Ayúdale a entender mejor los conceptos y a profundizar en el tema.",
            TipoSesionIA.Roleplay => "\n\nContexto: El estudiante está en una sesión de roleplay. " +
                "Actúa como un personaje del mundo laboral para que practique sus habilidades. " +
                "Mantente en personaje y da feedback constructivo después de cada interacción.",
            TipoSesionIA.ConsultaLibre => "\n\nContexto: El estudiante está en consulta libre. " +
                "Puede preguntar sobre cualquier tema de habilidades profesionales. " +
                "Sé abierta y explora los temas que le interesen.",
            TipoSesionIA.Autoevaluacion => """

                Contexto: Eres Aria, evaluadora experta. Tu objetivo es DIAGNOSTICAR el nivel real del usuario.
                - Haz preguntas abiertas, una a la vez.
                - Pide ejemplos concretos de su vida laboral.
                - No juzgues, sé curiosa y empática.
                - Después de 5-7 preguntas, da un diagnóstico honesto pero constructivo.
                - Identifica: 2 fortalezas, 2 áreas de mejora, 1 punto ciego.
                - Termina con un resumen estructurado del nivel detectado.
                """,
            TipoSesionIA.CasoEstudio => """

                Contexto: Eres Aria, analista de casos. Presenta el caso paso a paso.
                - Primero describe la situación sin revelar el desenlace.
                - Pregunta al usuario qué haría él en cada punto de decisión.
                - Después revela qué pasó realmente y por qué.
                - Guía al usuario a extraer 2-3 principios aplicables.
                - Conecta con el framework teórico de la lección anterior.
                """,
            TipoSesionIA.PracticaGuiada => """

                Contexto: Eres Aria actuando como un personaje específico. El usuario debe practicar una habilidad concreta.
                - Mantente en personaje durante toda la conversación.
                - Sube la dificultad gradualmente (empezar fácil, complicar).
                - Después de 4-5 intercambios, sal del personaje y da feedback:
                  * Qué hizo bien (específico, con cita textual).
                  * Qué podría mejorar (con ejemplo de cómo se vería mejor).
                  * Ofrece repetir con una variante más difícil.
                """,
            TipoSesionIA.Reflexion => """

                Contexto: Eres Aria, coach reflexivo. Guía al usuario por el Ciclo de Gibbs:
                1. Descripción: "¿Qué pasó exactamente en la práctica?"
                2. Sentimientos: "¿Qué sentiste durante la conversación?"
                3. Evaluación: "¿Qué salió bien y qué no?"
                4. Análisis: "¿Por qué crees que reaccionaste así?"
                5. Conclusión: "¿Qué aprendiste que no sabías antes?"
                6. Plan: "¿Qué harás diferente la próxima vez?"
                Haz una pregunta a la vez. Escucha antes de avanzar al siguiente paso.
                """,
            TipoSesionIA.PlanAccion => """

                Contexto: Eres Aria, coach de acción. Ayuda al usuario a crear un compromiso SMART:
                - Específico: ¿Qué habilidad vas a practicar exactamente?
                - Medible: ¿Cómo sabrás que lo hiciste?
                - Con quién: ¿En qué situación real lo vas a aplicar?
                - Cuándo: ¿Qué día/hora específica?
                - Seguimiento: "La próxima vez que nos veamos, te preguntaré cómo fue."
                No avances hasta que cada elemento sea concreto y realista.
                """,
            TipoSesionIA.Capstone => """

                Contexto: Eres Aria, mentora de proyecto integrador final.
                - Guía paso a paso pero NO des las respuestas.
                - Haz preguntas que le obliguen a pensar.
                - Valida cada paso antes de avanzar al siguiente.
                - Al final, evalúa el plan con rúbrica: Profundidad, Viabilidad, Creatividad, Impacto.
                - Sé exigente pero constructiva. Este es el nivel más alto.
                """,
            _ => ""
        };

        if (leccionId.HasValue)
        {
            contextoAdicional += $"\n\nLección asociada ID: {leccionId.Value}. Adapta tus respuestas al tema de esta lección.";
        }

        return promptBase + contextoAdicional;
    }

    private static string GenerarMensajeBienvenida(TipoSesionIA tipoSesion)
    {
        return tipoSesion switch
        {
            TipoSesionIA.RepasoDeLeccion =>
                "¡Hola! Soy **Aria**, tu instructora en SkillUp Academy. " +
                "Estoy aquí para ayudarte a profundizar en esta lección. " +
                "¿Qué concepto te gustaría explorar o qué dudas tienes?",
            TipoSesionIA.Roleplay =>
                "¡Bienvenido a la sesión de roleplay! Soy **Aria** y voy a interpretar un personaje " +
                "para que puedas practicar tus habilidades en un entorno seguro. " +
                "Cuando estés listo, cuéntame qué escenario te gustaría practicar.",
            TipoSesionIA.ConsultaLibre =>
                "¡Hola! Soy **Aria**, tu compañera de aprendizaje. " +
                "En esta sesión libre puedes preguntarme sobre cualquier habilidad profesional: " +
                "comunicación, liderazgo, trabajo en equipo, inteligencia emocional, networking o persuasión. " +
                "¿Por dónde quieres empezar?",
            TipoSesionIA.Autoevaluacion =>
                "¡Hola! Soy **Aria** y voy a ayudarte a descubrir tu nivel real de comunicación. " +
                "No hay respuestas correctas ni incorrectas — solo quiero conocer cómo te comunicas en tu día a día. " +
                "Voy a hacerte algunas preguntas abiertas. ¿Empezamos?",
            TipoSesionIA.CasoEstudio =>
                "¡Hola! Soy **Aria** y hoy vamos a analizar un caso real juntos. " +
                "Te voy a contar una situación paso a paso y quiero que me digas qué harías tú en cada momento. " +
                "Al final, veremos qué pasó realmente y qué podemos aprender. ¿Listo?",
            TipoSesionIA.PracticaGuiada =>
                "¡Hola! Soy **Aria** y en esta sesión voy a actuar como un personaje para que practiques. " +
                "Empezaremos con algo sencillo e iremos subiendo la dificultad. " +
                "Al final te daré feedback detallado. ¡Vamos allá!",
            TipoSesionIA.Reflexion =>
                "¡Hola! Soy **Aria** y esta es tu sesión de reflexión. " +
                "Vamos a revisar juntos lo que has experimentado y aprendido. " +
                "Te guiaré paso a paso. Empecemos: **¿Qué pasó exactamente en tu última práctica?**",
            TipoSesionIA.PlanAccion =>
                "¡Hola! Soy **Aria** y vamos a crear tu compromiso de acción. " +
                "No un propósito vago, sino algo concreto que vas a hacer esta semana. " +
                "Para empezar: **¿Qué habilidad específica quieres practicar?**",
            TipoSesionIA.Capstone =>
                "¡Hola! Soy **Aria** y bienvenido al proyecto integrador final. " +
                "Aquí vas a demostrar todo lo que has aprendido diseñando algo real. " +
                "Yo te guiaré con preguntas, pero las respuestas son tuyas. ¿Empezamos?",
            _ =>
                "¡Hola! Soy **Aria** de SkillUp Academy. ¿En qué puedo ayudarte hoy?"
        };
    }

    private static List<string> GenerarSugerencias(TipoSesionIA tipoSesion, string ultimoMensaje)
    {
        return tipoSesion switch
        {
            TipoSesionIA.RepasoDeLeccion => new List<string>
            {
                "¿Puedes darme un ejemplo práctico?",
                "¿Cómo aplico esto en mi trabajo?",
                "¿Qué errores comunes debo evitar?"
            },
            TipoSesionIA.Roleplay => new List<string>
            {
                "Continuemos con el escenario",
                "¿Cómo lo hice? Dame feedback",
                "Quiero intentar un enfoque diferente"
            },
            TipoSesionIA.ConsultaLibre => new List<string>
            {
                "Háblame sobre comunicación efectiva",
                "¿Cómo puedo mejorar mi liderazgo?",
                "Dame consejos para networking"
            },
            TipoSesionIA.Autoevaluacion => new List<string>
            {
                "Te cuento un ejemplo de mi trabajo",
                "No estoy seguro, ¿puedes reformular?",
                "¿Cuál es mi diagnóstico hasta ahora?"
            },
            TipoSesionIA.CasoEstudio => new List<string>
            {
                "Yo habría hecho algo diferente",
                "¿Qué pasó después?",
                "¿Qué principio puedo extraer de esto?"
            },
            TipoSesionIA.PracticaGuiada => new List<string>
            {
                "Continuemos la práctica",
                "Dame feedback de lo que hice",
                "Quiero intentar con más dificultad"
            },
            TipoSesionIA.Reflexion => new List<string>
            {
                "Me sentí incómodo en ese momento",
                "Creo que lo hice bien porque...",
                "La próxima vez haría algo diferente"
            },
            TipoSesionIA.PlanAccion => new List<string>
            {
                "Lo haré en mi próxima reunión",
                "¿Es suficientemente concreto?",
                "Necesito ayuda para definir el cuándo"
            },
            TipoSesionIA.Capstone => new List<string>
            {
                "Este es mi plan inicial",
                "¿Qué me falta considerar?",
                "¿Cómo puedo mejorarlo?"
            },
            _ => new List<string>
            {
                "Cuéntame más",
                "¿Qué más puedo aprender?",
                "Dame un consejo práctico"
            }
        };
    }
}
