using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de Text-to-Speech para el avatar Aria.
/// En modo MVP usa Web Speech API del navegador. Con Azure Speech configurado, genera audio server-side.
/// </summary>
public class ServicioTts : IServicioTts
{
    private readonly IConfiguration _configuracion;
    private readonly ILogger<ServicioTts> _logger;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Constructor del servicio TTS.
    /// </summary>
    public ServicioTts(
        IConfiguration configuracion,
        ILogger<ServicioTts> logger,
        HttpClient httpClient)
    {
        _configuracion = configuracion;
        _logger = logger;
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<byte[]> GenerarAudioAsync(string texto, string? voz = null, decimal velocidad = 1.0m)
    {
        string proveedor = _configuracion["Tts:Proveedor"] ?? "WebSpeechApi";

        if (proveedor == "AzureSpeech")
        {
            string? azureKey = _configuracion["Tts:AzureSpeechKey"];
            string azureRegion = _configuracion["Tts:AzureSpeechRegion"] ?? "westeurope";

            if (!string.IsNullOrWhiteSpace(azureKey))
            {
                return await GenerarAudioAzureAsync(texto, voz, velocidad, azureKey, azureRegion);
            }

            _logger.LogWarning("Azure Speech configurado pero sin API key. Fallback a modo vacío.");
        }

        // MVP: El frontend usa Web Speech API. Devolvemos array vacío.
        _logger.LogDebug("TTS en modo WebSpeechApi — el audio se genera en el navegador del cliente.");
        return await Task.FromResult(Array.Empty<byte>());
    }

    /// <inheritdoc />
    public async Task<Stream> GenerarAudioStreamAsync(string texto, string? voz = null, decimal velocidad = 1.0m)
    {
        byte[] audio = await GenerarAudioAsync(texto, voz, velocidad);
        return new MemoryStream(audio);
    }

    /// <inheritdoc />
    public async Task<bool> EstaDisponibleAsync()
    {
        string proveedor = _configuracion["Tts:Proveedor"] ?? "WebSpeechApi";

        if (proveedor == "AzureSpeech")
        {
            string? azureKey = _configuracion["Tts:AzureSpeechKey"];
            return await Task.FromResult(!string.IsNullOrWhiteSpace(azureKey));
        }

        // WebSpeechApi es client-side, el servidor no lo "provee"
        return await Task.FromResult(false);
    }

    private async Task<byte[]> GenerarAudioAzureAsync(string texto, string? voz, decimal velocidad,
        string apiKey, string region)
    {
        string vozFinal = voz ?? _configuracion["Tts:VozPorDefecto"] ?? "es-ES-ElviraNeural";

        // Construir SSML (Speech Synthesis Markup Language)
        // La velocidad se expresa como porcentaje: 1.0 = "0%", 1.25 = "+25%", 0.75 = "-25%"
        int porcentajeVelocidad = (int)((velocidad - 1.0m) * 100);
        string signo = porcentajeVelocidad >= 0 ? "+" : "";
        string velocidadSsml = $"{signo}{porcentajeVelocidad}%";

        string ssml = $"""
            <speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='es-ES'>
                <voice name='{vozFinal}'>
                    <prosody rate='{velocidadSsml}'>
                        {EscaparXml(texto)}
                    </prosody>
                </voice>
            </speak>
            """;

        string url = $"https://{region}.tts.speech.microsoft.com/cognitiveservices/v1";

        HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Post, url);
        solicitud.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);
        solicitud.Content = new StringContent(ssml, Encoding.UTF8, "application/ssml+xml");
        solicitud.Headers.Add("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");

        try
        {
            HttpResponseMessage respuesta = await _httpClient.SendAsync(solicitud);

            if (!respuesta.IsSuccessStatusCode)
            {
                string error = await respuesta.Content.ReadAsStringAsync();
                _logger.LogError("Error de Azure Speech: {StatusCode} - {Error}",
                    respuesta.StatusCode, error);
                return Array.Empty<byte>();
            }

            byte[] audio = await respuesta.Content.ReadAsByteArrayAsync();
            _logger.LogInformation("Audio generado con Azure Speech: {Bytes} bytes para texto de {Caracteres} caracteres",
                audio.Length, texto.Length);
            return audio;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar audio con Azure Speech");
            return Array.Empty<byte>();
        }
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
