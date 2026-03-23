using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.DTOs.IA;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de chat con la IA (Aria) usando la API de Anthropic.
/// </summary>
public class ServicioChatIA : IServicioChatIA
{
    private readonly IRepositorioChatIA _repositorio;
    private readonly IServicioSeguridadIA _seguridadIA;
    private readonly IConfiguration _configuracion;
    private readonly ILogger<ServicioChatIA> _logger;
    private readonly HttpClient _httpClient;

    private const string UrlApiAnthropic = "https://api.anthropic.com/v1/messages";
    private const string VersionApi = "2023-06-01";
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
        IConfiguration configuracion,
        ILogger<ServicioChatIA> logger,
        HttpClient httpClient)
    {
        _repositorio = repositorio;
        _seguridadIA = seguridadIA;
        _configuracion = configuracion;
        _logger = logger;
        _httpClient = httpClient;

        string? apiKey = _configuracion["Anthropic:ApiKey"];
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
            _httpClient.DefaultRequestHeaders.Add("anthropic-version", VersionApi);
        }
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

        // Llamar a la API de Anthropic
        (string respuestaTexto, int tokensUsados) = await LlamarApiAnthropicAsync(historial, peticion.Mensaje);

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

        await foreach (string fragmento in LlamarApiAnthropicStreamAsync(historial, peticion.Mensaje, cancellationToken))
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

    private async IAsyncEnumerable<string> LlamarApiAnthropicStreamAsync(
        IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        string? apiKey = _configuracion["Anthropic:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogWarning("API key de Anthropic no configurada. Usando respuesta de fallback (stream).");
            string fallback = GenerarRespuestaFallback(mensajeNuevo);
            yield return fallback;
            yield return "{\"tokens\":0}";
            yield break;
        }

        string modelo = _configuracion["Anthropic:ModeloChat"] ?? "claude-sonnet-4-20250514";
        int maxTokens = int.TryParse(_configuracion["Anthropic:MaxTokens"], out int mt) ? mt : 1000;
        double temperatura = double.TryParse(_configuracion["Anthropic:Temperatura"], out double t) ? t : 0.7;

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
            model = modelo,
            max_tokens = maxTokens,
            temperature = temperatura,
            system = promptSistema,
            messages = mensajesApi,
            stream = true
        };

        string jsonPayload = JsonSerializer.Serialize(payload);

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, UrlApiAnthropic)
        {
            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        };

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
            yield return GenerarRespuestaFallback(mensajeNuevo);
            yield return "{\"tokens\":0}";
            yield break;
        }

        if (!respuesta!.IsSuccessStatusCode)
        {
            string cuerpoError = await respuesta.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Error de la API de Anthropic (stream): {StatusCode} - {Body}",
                respuesta.StatusCode, cuerpoError);
            yield return GenerarRespuestaFallback(mensajeNuevo);
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

            string? textoExtraido = ProcesarEventoStream(datos, ref tokensTotal);
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
    private string? ProcesarEventoStream(string datosJson, ref int tokensTotal)
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

    private async Task<(string Respuesta, int TokensUsados)> LlamarApiAnthropicAsync(
        IReadOnlyList<MensajeChatIA> historial, string mensajeNuevo)
    {
        string? apiKey = _configuracion["Anthropic:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogWarning("API key de Anthropic no configurada. Usando respuesta de fallback.");
            return (GenerarRespuestaFallback(mensajeNuevo), 0);
        }

        string modelo = _configuracion["Anthropic:ModeloChat"] ?? "claude-sonnet-4-20250514";
        int maxTokens = int.TryParse(_configuracion["Anthropic:MaxTokens"], out int mt) ? mt : 1000;
        double temperatura = double.TryParse(_configuracion["Anthropic:Temperatura"], out double t) ? t : 0.7;

        // Extraer prompt del sistema del historial
        string promptSistema = historial
            .FirstOrDefault(m => m.Rol == "system")?.Contenido ?? PromptSistemaAria;

        // Construir mensajes para la API (sin el system message)
        List<object> mensajesApi = new List<object>();
        foreach (MensajeChatIA mensaje in historial.Where(m => m.Rol != "system"))
        {
            mensajesApi.Add(new { role = mensaje.Rol, content = mensaje.Contenido });
        }
        // Añadir el mensaje nuevo
        mensajesApi.Add(new { role = "user", content = mensajeNuevo });

        object payload = new
        {
            model = modelo,
            max_tokens = maxTokens,
            temperature = temperatura,
            system = promptSistema,
            messages = mensajesApi
        };

        string jsonPayload = JsonSerializer.Serialize(payload);
        StringContent contenido = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage respuesta = await _httpClient.PostAsync(UrlApiAnthropic, contenido);
            string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();

            if (!respuesta.IsSuccessStatusCode)
            {
                _logger.LogError("Error de la API de Anthropic: {StatusCode} - {Body}",
                    respuesta.StatusCode, jsonRespuesta);
                return (GenerarRespuestaFallback(mensajeNuevo), 0);
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al llamar a la API de Anthropic");
            return (GenerarRespuestaFallback(mensajeNuevo), 0);
        }
    }

    private static string GenerarRespuestaFallback(string mensajeUsuario)
    {
        return "¡Gracias por tu mensaje! En este momento estoy en modo de demostración. " +
               "Cuando se configure la API key de Anthropic, podré mantener conversaciones completas contigo. " +
               "Mientras tanto, te animo a seguir explorando las lecciones y practicando los ejercicios. " +
               "¿Hay algo específico sobre habilidades profesionales que te gustaría aprender?";
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
            _ => new List<string>
            {
                "Cuéntame más",
                "¿Qué más puedo aprender?",
                "Dame un consejo práctico"
            }
        };
    }
}
