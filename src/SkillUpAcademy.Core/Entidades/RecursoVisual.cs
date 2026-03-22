using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Recurso visual (imagen, diagrama, icono) asociado a una escena.
/// </summary>
public class RecursoVisual
{
    public int Id { get; set; }

    /// <summary>FK a la escena (nullable para recursos de biblioteca general).</summary>
    public int? EscenaLeccionId { get; set; }

    /// <summary>Tipo de recurso.</summary>
    public TipoRecursoVisual TipoRecurso { get; set; }

    /// <summary>Nombre descriptivo del recurso.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>URL o path del recurso.</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>Texto alternativo para accesibilidad.</summary>
    public string? TextoAlternativo { get; set; }

    /// <summary>Etiquetas para búsqueda (JSON array).</summary>
    public string? Etiquetas { get; set; }

    /// <summary>Tamaño en bytes.</summary>
    public long TamanoBytes { get; set; }

    /// <summary>Tipo MIME.</summary>
    public string? TipoMime { get; set; }

    /// <summary>Fecha de creación.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Navegación
    public EscenaLeccion? EscenaLeccion { get; set; }
}
