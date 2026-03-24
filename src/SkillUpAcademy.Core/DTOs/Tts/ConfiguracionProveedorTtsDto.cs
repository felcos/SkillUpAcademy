namespace SkillUpAcademy.Core.DTOs.Tts;

/// <summary>
/// DTO completo del proveedor TTS para el panel admin (incluye API key enmascarada).
/// </summary>
public record ConfiguracionProveedorTtsDto(
    int Id,
    string Tipo,
    string NombreVisible,
    string? Descripcion,
    bool Habilitado,
    bool TieneApiKey,
    string? Region,
    string VozPorDefecto,
    int Orden,
    int CantidadVoces,
    DateTime FechaCreacion,
    DateTime? FechaActualizacion
);

/// <summary>
/// Petición para crear o actualizar un proveedor TTS desde admin.
/// </summary>
public class PeticionGuardarProveedorTts
{
    /// <summary>Nombre visible para los usuarios.</summary>
    public string NombreVisible { get; set; } = string.Empty;

    /// <summary>Descripción opcional.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Habilitar o deshabilitar.</summary>
    public bool Habilitado { get; set; }

    /// <summary>API key (se guarda solo si se proporciona, no se sobreescribe si es null).</summary>
    public string? ApiKey { get; set; }

    /// <summary>Región del servicio (solo Azure).</summary>
    public string? Region { get; set; }

    /// <summary>Voz por defecto del proveedor.</summary>
    public string? VozPorDefecto { get; set; }

    /// <summary>Orden de prioridad.</summary>
    public int? Orden { get; set; }
}
