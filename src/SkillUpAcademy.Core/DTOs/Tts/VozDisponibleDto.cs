namespace SkillUpAcademy.Core.DTOs.Tts;

/// <summary>
/// Voz disponible para el usuario.
/// </summary>
public record VozDisponibleDto(
    string IdVoz,
    string Nombre,
    string Idioma,
    string Genero,
    string Proveedor,
    string? DescripcionPreview
);

/// <summary>
/// Configuración TTS visible para el usuario (sin API keys).
/// </summary>
public record ConfiguracionTtsUsuarioDto(
    IReadOnlyList<ProveedorTtsPublicoDto> Proveedores,
    IReadOnlyList<VozDisponibleDto> Voces,
    string? VozSeleccionada,
    decimal VelocidadVoz,
    string ProveedorPreferido
);

/// <summary>
/// Proveedor TTS visible para el usuario (sin datos sensibles).
/// </summary>
public record ProveedorTtsPublicoDto(
    string Tipo,
    string NombreVisible,
    string? Descripcion
);

/// <summary>
/// Petición para actualizar las preferencias de voz del usuario.
/// </summary>
public class PeticionActualizarVoz
{
    /// <summary>ID de la voz seleccionada (ej: "es-ES-ElviraNeural").</summary>
    public string? VozPreferida { get; set; }

    /// <summary>Velocidad de la voz (0.5 a 2.0).</summary>
    public decimal? VelocidadVoz { get; set; }

    /// <summary>Proveedor preferido (WebSpeechApi, AzureSpeech, ElevenLabs).</summary>
    public string? ProveedorPreferido { get; set; }
}

/// <summary>
/// Petición para sintetizar texto a audio.
/// </summary>
public record PeticionSintetizar(
    string Texto,
    string? Voz,
    decimal? Velocidad
);
