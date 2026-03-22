namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de Text-to-Speech para el avatar Aria.
/// </summary>
public interface IServicioTts
{
    /// <summary>Genera audio a partir de texto.</summary>
    Task<byte[]> GenerarAudioAsync(string texto, string? voz = null, decimal velocidad = 1.0m);

    /// <summary>Genera audio en streaming.</summary>
    Task<Stream> GenerarAudioStreamAsync(string texto, string? voz = null, decimal velocidad = 1.0m);

    /// <summary>Verifica si el servicio de TTS está disponible.</summary>
    Task<bool> EstaDisponibleAsync();
}
