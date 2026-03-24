using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.DTOs.Tts;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de Text-to-Speech multi-proveedor.
/// Soporta Azure Speech Services, ElevenLabs y fallback a Web Speech API del navegador.
/// Los proveedores se configuran desde el panel admin (tabla proveedores_tts).
/// </summary>
public class ServicioTts : IServicioTts
{
    private readonly AppDbContext _contexto;
    private readonly UserManager<UsuarioApp> _userManager;
    private readonly ILogger<ServicioTts> _logger;
    private readonly HttpClient _httpClient;

    private const string TextoPreview = "Hola, soy Aria, tu instructora en SkillUp Academy. Estoy aquí para ayudarte a mejorar tus habilidades.";

    /// <summary>
    /// Constructor del servicio TTS multi-proveedor.
    /// </summary>
    public ServicioTts(
        AppDbContext contexto,
        UserManager<UsuarioApp> userManager,
        ILogger<ServicioTts> logger,
        HttpClient httpClient)
    {
        _contexto = contexto;
        _userManager = userManager;
        _logger = logger;
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<byte[]> GenerarAudioAsync(string texto, string? voz = null, decimal velocidad = 1.0m, string? proveedor = null)
    {
        ProveedorTts? proveedorTts = await ResolverProveedorAsync(proveedor);

        if (proveedorTts == null)
        {
            _logger.LogDebug("Sin proveedor TTS server-side habilitado. Fallback a Web Speech API del navegador.");
            return Array.Empty<byte>();
        }

        string vozFinal = voz ?? proveedorTts.VozPorDefecto;

        return proveedorTts.Tipo switch
        {
            TipoProveedorTts.AzureSpeech => await GenerarAudioAzureAsync(texto, vozFinal, velocidad, proveedorTts),
            TipoProveedorTts.ElevenLabs => await GenerarAudioElevenLabsAsync(texto, vozFinal, velocidad, proveedorTts),
            _ => Array.Empty<byte>()
        };
    }

    /// <inheritdoc />
    public async Task<Stream> GenerarAudioStreamAsync(string texto, string? voz = null, decimal velocidad = 1.0m, string? proveedor = null)
    {
        byte[] audio = await GenerarAudioAsync(texto, voz, velocidad, proveedor);
        return new MemoryStream(audio);
    }

    /// <inheritdoc />
    public async Task<bool> EstaDisponibleAsync()
    {
        return await _contexto.ProveedoresTts
            .AnyAsync(p => p.Habilitado && !string.IsNullOrWhiteSpace(p.ApiKey));
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<VozDisponibleDto>> ObtenerVocesDisponiblesAsync()
    {
        List<ProveedorTts> proveedores = await _contexto.ProveedoresTts
            .Where(p => p.Habilitado)
            .OrderBy(p => p.Orden)
            .ToListAsync();

        List<VozDisponibleDto> voces = new List<VozDisponibleDto>();

        foreach (ProveedorTts proveedor in proveedores)
        {
            IReadOnlyList<VozDisponibleDto> vocesProveedor = ObtenerVocesDeProveedor(proveedor);
            voces.AddRange(vocesProveedor);
        }

        // Siempre incluir las voces del navegador (Web Speech API)
        voces.AddRange(ObtenerVocesWebSpeech());

        return voces;
    }

    /// <inheritdoc />
    public async Task<ConfiguracionTtsUsuarioDto> ObtenerConfiguracionUsuarioAsync(Guid usuarioId)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        List<ProveedorTts> proveedoresHabilitados = await _contexto.ProveedoresTts
            .Where(p => p.Habilitado)
            .OrderBy(p => p.Orden)
            .ToListAsync();

        List<ProveedorTtsPublicoDto> proveedoresPublicos = new List<ProveedorTtsPublicoDto>();

        // Siempre incluir Web Speech API como opción
        proveedoresPublicos.Add(new ProveedorTtsPublicoDto(
            "WebSpeechApi",
            "Voz del sistema",
            "Usa la voz de tu navegador/sistema operativo. Sin coste, disponible siempre."
        ));

        foreach (ProveedorTts proveedor in proveedoresHabilitados)
        {
            proveedoresPublicos.Add(new ProveedorTtsPublicoDto(
                proveedor.Tipo.ToString(),
                proveedor.NombreVisible,
                proveedor.Descripcion
            ));
        }

        IReadOnlyList<VozDisponibleDto> voces = await ObtenerVocesDisponiblesAsync();

        return new ConfiguracionTtsUsuarioDto(
            proveedoresPublicos,
            voces,
            usuario.VozPreferida,
            usuario.VelocidadVoz,
            usuario.ProveedorTtsPreferido
        );
    }

    /// <inheritdoc />
    public async Task ActualizarPreferenciaVozAsync(Guid usuarioId, PeticionActualizarVoz peticion)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        if (!string.IsNullOrWhiteSpace(peticion.VozPreferida))
            usuario.VozPreferida = peticion.VozPreferida;

        if (peticion.VelocidadVoz.HasValue)
        {
            decimal velocidad = Math.Clamp(peticion.VelocidadVoz.Value, 0.5m, 2.0m);
            usuario.VelocidadVoz = velocidad;
        }

        if (!string.IsNullOrWhiteSpace(peticion.ProveedorPreferido))
            usuario.ProveedorTtsPreferido = peticion.ProveedorPreferido;

        await _userManager.UpdateAsync(usuario);
        _logger.LogInformation("Preferencias TTS actualizadas para usuario {UsuarioId}: voz={Voz}, velocidad={Velocidad}, proveedor={Proveedor}",
            usuarioId, usuario.VozPreferida, usuario.VelocidadVoz, usuario.ProveedorTtsPreferido);
    }

    /// <inheritdoc />
    public async Task<byte[]> GenerarPreviewVozAsync(string idVoz, string proveedor)
    {
        return await GenerarAudioAsync(TextoPreview, idVoz, 1.0m, proveedor);
    }

    // ======================== AZURE SPEECH ========================

    private async Task<byte[]> GenerarAudioAzureAsync(string texto, string voz, decimal velocidad, ProveedorTts proveedor)
    {
        if (string.IsNullOrWhiteSpace(proveedor.ApiKey))
        {
            _logger.LogWarning("Azure Speech habilitado pero sin API key.");
            return Array.Empty<byte>();
        }

        string region = proveedor.Region ?? "westeurope";
        int porcentajeVelocidad = (int)((velocidad - 1.0m) * 100);
        string signo = porcentajeVelocidad >= 0 ? "+" : "";
        string velocidadSsml = $"{signo}{porcentajeVelocidad}%";

        string ssml = $"""
            <speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='es-ES'>
                <voice name='{EscaparXml(voz)}'>
                    <prosody rate='{velocidadSsml}'>
                        {EscaparXml(texto)}
                    </prosody>
                </voice>
            </speak>
            """;

        string url = $"https://{region}.tts.speech.microsoft.com/cognitiveservices/v1";

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url);
        solicitud.Headers.Add("Ocp-Apim-Subscription-Key", proveedor.ApiKey);
        solicitud.Content = new StringContent(ssml, Encoding.UTF8, "application/ssml+xml");
        solicitud.Headers.Add("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");

        try
        {
            HttpResponseMessage respuesta = await _httpClient.SendAsync(solicitud);

            if (!respuesta.IsSuccessStatusCode)
            {
                string error = await respuesta.Content.ReadAsStringAsync();
                _logger.LogError("Error de Azure Speech: {StatusCode} - {Error}", respuesta.StatusCode, error);
                return Array.Empty<byte>();
            }

            byte[] audio = await respuesta.Content.ReadAsByteArrayAsync();
            _logger.LogInformation("Audio Azure generado: {Bytes} bytes, voz={Voz}, caracteres={Caracteres}",
                audio.Length, voz, texto.Length);
            return audio;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar audio con Azure Speech");
            return Array.Empty<byte>();
        }
    }

    // ======================== ELEVENLABS ========================

    private async Task<byte[]> GenerarAudioElevenLabsAsync(string texto, string vozId, decimal velocidad, ProveedorTts proveedor)
    {
        if (string.IsNullOrWhiteSpace(proveedor.ApiKey))
        {
            _logger.LogWarning("ElevenLabs habilitado pero sin API key.");
            return Array.Empty<byte>();
        }

        // ElevenLabs usa stability y similarity_boost para controlar la voz
        // La velocidad no se controla directamente, pero se puede ajustar con el parámetro speed (v2.5+)
        string url = $"https://api.elevenlabs.io/v1/text-to-speech/{vozId}";

        object cuerpo = new
        {
            text = texto,
            model_id = "eleven_multilingual_v2",
            voice_settings = new
            {
                stability = 0.5,
                similarity_boost = 0.8,
                style = 0.0,
                use_speaker_boost = true
            }
        };

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url);
        solicitud.Headers.Add("xi-api-key", proveedor.ApiKey);
        solicitud.Content = new StringContent(
            JsonSerializer.Serialize(cuerpo),
            Encoding.UTF8,
            "application/json"
        );

        try
        {
            HttpResponseMessage respuesta = await _httpClient.SendAsync(solicitud);

            if (!respuesta.IsSuccessStatusCode)
            {
                string error = await respuesta.Content.ReadAsStringAsync();
                _logger.LogError("Error de ElevenLabs: {StatusCode} - {Error}", respuesta.StatusCode, error);
                return Array.Empty<byte>();
            }

            byte[] audio = await respuesta.Content.ReadAsByteArrayAsync();
            _logger.LogInformation("Audio ElevenLabs generado: {Bytes} bytes, vozId={VozId}, caracteres={Caracteres}",
                audio.Length, vozId, texto.Length);
            return audio;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar audio con ElevenLabs");
            return Array.Empty<byte>();
        }
    }

    // ======================== HELPERS ========================

    private async Task<ProveedorTts?> ResolverProveedorAsync(string? proveedorSolicitado)
    {
        if (!string.IsNullOrWhiteSpace(proveedorSolicitado))
        {
            if (Enum.TryParse<TipoProveedorTts>(proveedorSolicitado, ignoreCase: true, out TipoProveedorTts tipo))
            {
                if (tipo == TipoProveedorTts.WebSpeechApi)
                    return null; // Web Speech se maneja en el frontend

                return await _contexto.ProveedoresTts
                    .FirstOrDefaultAsync(p => p.Tipo == tipo && p.Habilitado);
            }
        }

        // Retornar el proveedor habilitado de mayor prioridad (menor orden)
        return await _contexto.ProveedoresTts
            .Where(p => p.Habilitado && !string.IsNullOrWhiteSpace(p.ApiKey))
            .OrderBy(p => p.Orden)
            .FirstOrDefaultAsync();
    }

    private static IReadOnlyList<VozDisponibleDto> ObtenerVocesDeProveedor(ProveedorTts proveedor)
    {
        if (!string.IsNullOrWhiteSpace(proveedor.VocesDisponiblesJson))
        {
            try
            {
                List<VozDisponibleDto>? voces = JsonSerializer.Deserialize<List<VozDisponibleDto>>(
                    proveedor.VocesDisponiblesJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                return voces ?? ObtenerVocesPorDefectoDeProveedor(proveedor);
            }
            catch
            {
                // JSON corrupto, devolver las por defecto
            }
        }

        return ObtenerVocesPorDefectoDeProveedor(proveedor);
    }

    private static IReadOnlyList<VozDisponibleDto> ObtenerVocesPorDefectoDeProveedor(ProveedorTts proveedor)
    {
        return proveedor.Tipo switch
        {
            TipoProveedorTts.AzureSpeech => new List<VozDisponibleDto>
            {
                new("es-ES-ElviraNeural", "Elvira (España)", "es-ES", "Femenino", "AzureSpeech", "Voz femenina española, cálida y profesional"),
                new("es-ES-AlvaroNeural", "Álvaro (España)", "es-ES", "Masculino", "AzureSpeech", "Voz masculina española, clara y formal"),
                new("es-ES-AbrilNeural", "Abril (España)", "es-ES", "Femenino", "AzureSpeech", "Voz femenina joven y dinámica"),
                new("es-ES-ArnauNeural", "Arnau (España)", "es-ES", "Masculino", "AzureSpeech", "Voz masculina joven y cercana"),
                new("es-ES-DarioNeural", "Darío (España)", "es-ES", "Masculino", "AzureSpeech", "Voz masculina madura y seria"),
                new("es-ES-EliasNeural", "Elías (España)", "es-ES", "Masculino", "AzureSpeech", "Voz masculina cálida y amigable"),
                new("es-ES-EstrellaNeural", "Estrella (España)", "es-ES", "Femenino", "AzureSpeech", "Voz femenina suave y elegante"),
                new("es-ES-IreneNeural", "Irene (España)", "es-ES", "Femenino", "AzureSpeech", "Voz femenina natural y expresiva"),
                new("es-MX-DaliaNeural", "Dalia (México)", "es-MX", "Femenino", "AzureSpeech", "Voz femenina mexicana, natural"),
                new("es-MX-JorgeNeural", "Jorge (México)", "es-MX", "Masculino", "AzureSpeech", "Voz masculina mexicana, clara"),
                new("es-AR-ElenaNeural", "Elena (Argentina)", "es-AR", "Femenino", "AzureSpeech", "Voz femenina argentina"),
                new("es-CO-SalomeNeural", "Salomé (Colombia)", "es-CO", "Femenino", "AzureSpeech", "Voz femenina colombiana"),
            },
            TipoProveedorTts.ElevenLabs => new List<VozDisponibleDto>
            {
                new("21m00Tcm4TlvDq8ikWAM", "Rachel", "multilingual", "Femenino", "ElevenLabs", "Voz femenina cálida y versátil"),
                new("EXAVITQu4vr4xnSDxMaL", "Bella", "multilingual", "Femenino", "ElevenLabs", "Voz femenina suave y acogedora"),
                new("ErXwobaYiN019PkySvjV", "Antoni", "multilingual", "Masculino", "ElevenLabs", "Voz masculina clara y profesional"),
                new("VR6AewLTigWG4xSOukaG", "Arnold", "multilingual", "Masculino", "ElevenLabs", "Voz masculina profunda y autoritaria"),
                new("pNInz6obpgDQGcFmaJgB", "Adam", "multilingual", "Masculino", "ElevenLabs", "Voz masculina natural y cercana"),
                new("yoZ06aMxZJJ28mfd3POQ", "Sam", "multilingual", "Masculino", "ElevenLabs", "Voz masculina joven y dinámica"),
            },
            _ => new List<VozDisponibleDto>()
        };
    }

    private static IReadOnlyList<VozDisponibleDto> ObtenerVocesWebSpeech()
    {
        return new List<VozDisponibleDto>
        {
            new("web-speech-default", "Voz del sistema", "es", "Variable", "WebSpeechApi", "La voz por defecto de tu navegador. Varía según sistema operativo."),
        };
    }

    private static string EscaparXml(string texto)
    {
        return texto
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");
    }
}
