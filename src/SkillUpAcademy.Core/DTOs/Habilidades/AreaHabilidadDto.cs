namespace SkillUpAcademy.Core.DTOs.Habilidades;

/// <summary>
/// Resumen de un área de habilidad para listados.
/// </summary>
public class AreaHabilidadDto
{
    /// <summary>Identificador del área.</summary>
    public int Id { get; set; }

    /// <summary>Slug único del área para URLs amigables.</summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>Título del área de habilidad.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Subtítulo descriptivo del área.</summary>
    public string? Subtitulo { get; set; }

    /// <summary>Icono representativo del área.</summary>
    public string? Icono { get; set; }

    /// <summary>Color primario en formato hexadecimal.</summary>
    public string? ColorPrimario { get; set; }

    /// <summary>Resumen del progreso del usuario en esta área.</summary>
    public ProgresoResumenDto? Progreso { get; set; }
}

/// <summary>
/// Resumen compacto del progreso en un área de habilidad.
/// </summary>
public class ProgresoResumenDto
{
    /// <summary>Cantidad de lecciones completadas.</summary>
    public int LeccionesCompletadas { get; set; }

    /// <summary>Cantidad total de lecciones en el área.</summary>
    public int LeccionesTotales { get; set; }

    /// <summary>Porcentaje de avance (0-100).</summary>
    public int Porcentaje { get; set; }

    /// <summary>Nivel actual del usuario en esta área.</summary>
    public int NivelActual { get; set; }
}
