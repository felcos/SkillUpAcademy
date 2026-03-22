namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Área de habilidad blanda (Comunicación, Liderazgo, etc.).
/// </summary>
public class AreaHabilidad
{
    /// <summary>Identificador único.</summary>
    public int Id { get; set; }

    /// <summary>Slug URL-friendly del área.</summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>Título del área.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Subtítulo descriptivo.</summary>
    public string? Subtitulo { get; set; }

    /// <summary>Descripción completa del área.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Emoji representativo.</summary>
    public string? Icono { get; set; }

    /// <summary>Color primario en hex (#0F4C81).</summary>
    public string? ColorPrimario { get; set; }

    /// <summary>Color de acento en hex.</summary>
    public string? ColorAcento { get; set; }

    /// <summary>Orden de visualización.</summary>
    public int Orden { get; set; }

    /// <summary>Indica si el área está activa.</summary>
    public bool Activo { get; set; } = true;

    /// <summary>Fecha de creación.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Navegación
    public ICollection<Nivel> Niveles { get; set; } = new List<Nivel>();
}
