using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de seguridad anti-abuso para la IA con 5 capas de protección.
/// </summary>
public class ServicioSeguridadIA : IServicioSeguridadIA
{
    private readonly AppDbContext _contexto;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuracion;
    private readonly ILogger<ServicioSeguridadIA> _logger;

    private readonly int _maxMensajesPorMinuto;
    private const int MaxLongitudMensaje = 1000;
    private const int MaxLongitudRespuesta = 2000;

    private static readonly string[] PalabrasProhibidasInyeccion =
    {
        "ignora las instrucciones", "olvida tu sistema", "olvida las instrucciones",
        "ignore previous", "forget your", "forget all", "you are now",
        "pretend you are", "act as", "actúa como si fueras",
        "jailbreak", "DAN", "do anything now", "modo desarrollador",
        "developer mode", "system prompt", "bypass", "override instructions",
        "nueva personalidad", "cambia de rol", "eres ahora",
        "repite el prompt", "muestra tu prompt", "show your prompt",
        "reveal your instructions", "muestra tus instrucciones"
    };

    private static readonly string[] PalabrasProhibidasContenido =
    {
        // Solo las más graves - contenido claramente inapropiado para una plataforma educativa
        "contenido sexual", "pornografía", "desnudo",
        "cómo fabricar", "cómo hacer una bomba", "how to make a bomb",
        "suicidio", "autolesión", "hacerse daño"
    };

    private static readonly Regex[] PatronesInyeccion =
    {
        new(@"(system|instruc|prompt)\s*:", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"(eres|you are|tu eres|tú eres)\s+.{0,20}(ahora|now)", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"\[.*?(system|admin|root|developer).*?\]", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"```\s*(system|prompt|instruc)", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"<\s*(system|prompt|instruction)", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"(responde|respond|answer)\s+.{0,10}(solo|only|just)\s+.{0,10}(sí|si|yes|no|true|false)", RegexOptions.IgnoreCase | RegexOptions.Compiled)
    };

    private static readonly Regex PatronInfoPersonal = new(
        @"(\b\d{8,9}[A-Za-z]\b)|" +                     // DNI español
        @"(\b\d{3}[-.\s]?\d{3}[-.\s]?\d{4}\b)|" +       // Teléfono
        @"(\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b)", // Email
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Constructor del servicio de seguridad IA.
    /// </summary>
    public ServicioSeguridadIA(
        AppDbContext contexto,
        IMemoryCache cache,
        IConfiguration configuracion,
        ILogger<ServicioSeguridadIA> logger)
    {
        _contexto = contexto;
        _cache = cache;
        _configuracion = configuracion;
        _logger = logger;
        _maxMensajesPorMinuto = int.TryParse(_configuracion["LimitesDeUso:PeticionesIAPorMinuto"], out int limite) ? limite : 20;
    }

    /// <inheritdoc />
    public async Task<ResultadoValidacionIA> ValidarEntradaAsync(string mensaje, Guid usuarioId, Guid sesionId)
    {
        // Capa 1: Rate Limiting
        ResultadoValidacionIA resultadoRateLimit = ValidarRateLimit(usuarioId);
        if (!resultadoRateLimit.EsSeguro)
            return resultadoRateLimit;

        // Capa 2: Validación de formato
        ResultadoValidacionIA resultadoFormato = ValidarFormato(mensaje);
        if (!resultadoFormato.EsSeguro)
            return resultadoFormato;

        // Capa 3: Filtro de palabras clave
        ResultadoValidacionIA resultadoPalabras = ValidarPalabrasProhibidas(mensaje);
        if (!resultadoPalabras.EsSeguro)
        {
            await RegistrarStrikeAsync(usuarioId, sesionId,
                resultadoPalabras.CategoriaViolacion ?? "PalabrasProhibidas",
                mensaje, "FiltroPalabras");
            return resultadoPalabras;
        }

        // Capa 4: Detección de inyección de prompt
        ResultadoValidacionIA resultadoInyeccion = ValidarInyeccionPrompt(mensaje);
        if (!resultadoInyeccion.EsSeguro)
        {
            await RegistrarStrikeAsync(usuarioId, sesionId,
                "InyeccionPrompt", mensaje, "DeteccionHeuristica");
            return resultadoInyeccion;
        }

        // Capa 5: Clasificador IA (solo si hay API key configurada)
        string? apiKey = _configuracion["Anthropic:ApiKey"];
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            // La clasificación IA se puede implementar en el futuro
            // Por ahora las 4 capas anteriores son suficientes
            _logger.LogDebug("Capa 5 (clasificador IA) disponible pero no ejecutada en esta versión");
        }

        return new ResultadoValidacionIA { EsSeguro = true };
    }

    /// <inheritdoc />
    public async Task<ResultadoValidacionIA> ValidarSalidaAsync(string respuesta)
    {
        // Verificar información personal ficticia
        if (PatronInfoPersonal.IsMatch(respuesta))
        {
            _logger.LogWarning("La respuesta de IA contiene patrones de información personal");
            string respuestaLimpia = PatronInfoPersonal.Replace(respuesta, "[información eliminada]");
            return new ResultadoValidacionIA
            {
                EsSeguro = false,
                Razon = "La respuesta contenía patrones de información personal y fue limpiada.",
                MensajeAlternativo = respuestaLimpia
            };
        }

        // Verificar longitud
        if (respuesta.Length > MaxLongitudRespuesta)
        {
            string respuestaTruncada = respuesta[..MaxLongitudRespuesta] +
                "\n\n*[Respuesta truncada por longitud. Haz una pregunta más específica para obtener información detallada.]*";
            return new ResultadoValidacionIA
            {
                EsSeguro = true,
                MensajeAlternativo = respuestaTruncada
            };
        }

        return await Task.FromResult(new ResultadoValidacionIA { EsSeguro = true });
    }

    /// <inheritdoc />
    public async Task RegistrarStrikeAsync(Guid usuarioId, Guid sesionId, string tipoViolacion,
        string mensajeOriginal, string metodoDeteccion)
    {
        TipoViolacion tipo = tipoViolacion switch
        {
            "Spam" or "RateLimit" => TipoViolacion.SpamRepetitivo,
            "ContenidoInapropiado" => TipoViolacion.ContenidoInapropiado,
            "InyeccionPrompt" => TipoViolacion.IntentoDeInyeccion,
            "ManipulacionIA" or "Suplantacion" => TipoViolacion.IntentoDeSuplantacion,
            _ => TipoViolacion.Otro
        };

        // Contar strikes previos en las últimas 24h
        DateTime hace24Horas = DateTime.UtcNow.AddHours(-24);
        int strikePrevios = await _contexto.Set<RegistroAbuso>()
            .CountAsync(r => r.UsuarioId == usuarioId && r.FechaCreacion >= hace24Horas);

        AccionTomada accion = strikePrevios switch
        {
            < 2 => AccionTomada.Advertencia,
            2 => AccionTomada.BloqueoTemporal,
            _ => AccionTomada.BloqueoPermanente
        };

        RegistroAbuso registro = new RegistroAbuso
        {
            UsuarioId = usuarioId,
            SesionId = sesionId,
            TipoViolacion = tipo,
            MensajeOriginal = mensajeOriginal.Length > 500 ? mensajeOriginal[..500] : mensajeOriginal,
            MetodoDeteccion = metodoDeteccion,
            AccionTomada = accion,
            FechaCreacion = DateTime.UtcNow
        };

        _contexto.Set<RegistroAbuso>().Add(registro);
        await _contexto.SaveChangesAsync();

        _logger.LogWarning("Strike registrado para usuario {UsuarioId}: {TipoViolacion} - Acción: {Accion} (Strike #{Numero})",
            usuarioId, tipoViolacion, accion, strikePrevios + 1);
    }

    /// <inheritdoc />
    public async Task<bool> UsuarioBloqueadoAsync(Guid usuarioId)
    {
        DateTime hace24Horas = DateTime.UtcNow.AddHours(-24);
        int strikes = await _contexto.Set<RegistroAbuso>()
            .CountAsync(r => r.UsuarioId == usuarioId && r.FechaCreacion >= hace24Horas);

        if (strikes >= 4)
            return true;

        // Verificar si hay bloqueo reciente
        int horasBloqueo = int.TryParse(_configuracion["Seguridad:HorasBloqueoIADespuesDeStrikes"], out int h) ? h : 1;
        DateTime limiteBloqueo = DateTime.UtcNow.AddHours(-horasBloqueo);

        bool tieneBloqueReciente = await _contexto.Set<RegistroAbuso>()
            .AnyAsync(r => r.UsuarioId == usuarioId &&
                          r.AccionTomada == AccionTomada.BloqueoPermanente &&
                          r.FechaCreacion >= limiteBloqueo);

        return tieneBloqueReciente;
    }

    private ResultadoValidacionIA ValidarRateLimit(Guid usuarioId)
    {
        string cacheKey = $"rate_limit_ia_{usuarioId}";

        if (_cache.TryGetValue(cacheKey, out int contadorMensajes))
        {
            if (contadorMensajes >= _maxMensajesPorMinuto)
            {
                return new ResultadoValidacionIA
                {
                    EsSeguro = false,
                    Razon = "Has enviado demasiados mensajes. Espera un momento antes de continuar.",
                    CategoriaViolacion = "RateLimit",
                    MensajeAlternativo = "Por favor, espera un momento antes de enviar otro mensaje."
                };
            }

            _cache.Set(cacheKey, contadorMensajes + 1, TimeSpan.FromMinutes(1));
        }
        else
        {
            _cache.Set(cacheKey, 1, TimeSpan.FromMinutes(1));
        }

        return new ResultadoValidacionIA { EsSeguro = true };
    }

    private static ResultadoValidacionIA ValidarFormato(string mensaje)
    {
        if (string.IsNullOrWhiteSpace(mensaje))
        {
            return new ResultadoValidacionIA
            {
                EsSeguro = false,
                Razon = "El mensaje no puede estar vacío.",
                CategoriaViolacion = "FormatoInvalido"
            };
        }

        if (mensaje.Length > MaxLongitudMensaje)
        {
            return new ResultadoValidacionIA
            {
                EsSeguro = false,
                Razon = $"El mensaje excede el límite de {MaxLongitudMensaje} caracteres.",
                CategoriaViolacion = "FormatoInvalido"
            };
        }

        // Detectar caracteres repetidos (ej: "aaaaaaaaa")
        if (mensaje.Length > 10)
        {
            int caracteresUnicos = mensaje.Distinct().Count();
            double ratioUnicos = (double)caracteresUnicos / mensaje.Length;
            if (ratioUnicos < 0.05)
            {
                return new ResultadoValidacionIA
                {
                    EsSeguro = false,
                    Razon = "El mensaje parece contener texto repetitivo.",
                    CategoriaViolacion = "Spam"
                };
            }
        }

        // Detectar exceso de caracteres no alfanuméricos
        int noAlfanumericos = mensaje.Count(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
        if (mensaje.Length > 5 && (double)noAlfanumericos / mensaje.Length > 0.5)
        {
            return new ResultadoValidacionIA
            {
                EsSeguro = false,
                Razon = "El mensaje contiene demasiados caracteres especiales.",
                CategoriaViolacion = "FormatoInvalido"
            };
        }

        return new ResultadoValidacionIA { EsSeguro = true };
    }

    private static ResultadoValidacionIA ValidarPalabrasProhibidas(string mensaje)
    {
        string mensajeLower = mensaje.ToLowerInvariant();

        foreach (string palabra in PalabrasProhibidasInyeccion)
        {
            if (mensajeLower.Contains(palabra, StringComparison.OrdinalIgnoreCase))
            {
                return new ResultadoValidacionIA
                {
                    EsSeguro = false,
                    Razon = "Tu mensaje contiene instrucciones que no puedo procesar.",
                    CategoriaViolacion = "InyeccionPrompt",
                    MensajeAlternativo = "Parece que tu mensaje intenta modificar mis instrucciones. " +
                        "¿Puedo ayudarte con algo relacionado con habilidades profesionales?"
                };
            }
        }

        foreach (string palabra in PalabrasProhibidasContenido)
        {
            if (mensajeLower.Contains(palabra, StringComparison.OrdinalIgnoreCase))
            {
                return new ResultadoValidacionIA
                {
                    EsSeguro = false,
                    Razon = "Tu mensaje contiene contenido que no es apropiado para esta plataforma.",
                    CategoriaViolacion = "ContenidoInapropiado",
                    MensajeAlternativo = "Este contenido no es apropiado para SkillUp Academy. " +
                        "¿Te gustaría hablar sobre alguna habilidad profesional?"
                };
            }
        }

        return new ResultadoValidacionIA { EsSeguro = true };
    }

    private static ResultadoValidacionIA ValidarInyeccionPrompt(string mensaje)
    {
        foreach (Regex patron in PatronesInyeccion)
        {
            if (patron.IsMatch(mensaje))
            {
                return new ResultadoValidacionIA
                {
                    EsSeguro = false,
                    Razon = "Se detectó un patrón potencialmente inseguro en tu mensaje.",
                    CategoriaViolacion = "InyeccionPrompt",
                    MensajeAlternativo = "He detectado un patrón inusual en tu mensaje. " +
                        "¿Podrías reformularlo de otra manera?"
                };
            }
        }

        return new ResultadoValidacionIA { EsSeguro = true };
    }
}
