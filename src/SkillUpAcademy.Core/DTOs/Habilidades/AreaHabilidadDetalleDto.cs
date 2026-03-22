namespace SkillUpAcademy.Core.DTOs.Habilidades;

/// <summary>
/// Detalle completo de un área de habilidad, incluyendo sus niveles.
/// </summary>
public class AreaHabilidadDetalleDto
{
    /// <summary>Identificador del área.</summary>
    public int Id { get; set; }

    /// <summary>Slug único del área para URLs amigables.</summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>Título del área de habilidad.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Subtítulo descriptivo del área.</summary>
    public string? Subtitulo { get; set; }

    /// <summary>Descripción extendida del área.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Icono representativo del área.</summary>
    public string? Icono { get; set; }

    /// <summary>Color primario en formato hexadecimal.</summary>
    public string? ColorPrimario { get; set; }

    /// <summary>Color de acento en formato hexadecimal.</summary>
    public string? ColorAcento { get; set; }

    /// <summary>Resumen del progreso del usuario en esta área.</summary>
    public ProgresoResumenDto? Progreso { get; set; }

    /// <summary>Lista de niveles del área.</summary>
    public IReadOnlyList<NivelDto> Niveles { get; set; } = new List<NivelDto>();
}
