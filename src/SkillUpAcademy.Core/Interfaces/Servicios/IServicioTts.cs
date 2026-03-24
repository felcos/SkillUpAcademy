using SkillUpAcademy.Core.DTOs.Tts;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de Text-to-Speech multi-proveedor para el avatar Aria.
/// Soporta Azure Speech, ElevenLabs y WebSpeech (fallback navegador).
/// </summary>
public interface IServicioTts
{
    /// <summary>Genera audio a partir de texto usando el proveedor indicado o el preferido del usuario.</summary>
    Task<byte[]> GenerarAudioAsync(string texto, string? voz = null, decimal velocidad = 1.0m, string? proveedor = null);

    /// <summary>Genera audio en streaming.</summary>
    Task<Stream> GenerarAudioStreamAsync(string texto, string? voz = null, decimal velocidad = 1.0m, string? proveedor = null);

    /// <summary>Verifica si algún proveedor server-side está disponible.</summary>
    Task<bool> EstaDisponibleAsync();

    /// <summary>Obtiene las voces disponibles de todos los proveedores habilitados.</summary>
    Task<IReadOnlyList<VozDisponibleDto>> ObtenerVocesDisponiblesAsync();

    /// <summary>Obtiene la configuración TTS visible para un usuario.</summary>
    Task<ConfiguracionTtsUsuarioDto> ObtenerConfiguracionUsuarioAsync(Guid usuarioId);

    /// <summary>Actualiza las preferencias de voz de un usuario.</summary>
    Task ActualizarPreferenciaVozAsync(Guid usuarioId, PeticionActualizarVoz peticion);

    /// <summary>Genera una preview corta de una voz específica.</summary>
    Task<byte[]> GenerarPreviewVozAsync(string idVoz, string proveedor);
}
