namespace SkillUpAcademy.Core.DTOs.Escenas;

/// <summary>
/// Escena visual dentro de una lección.
/// </summary>
public class EscenaDto
{
    /// <summary>Identificador de la escena.</summary>
    public int Id { get; set; }

    /// <summary>Orden de la escena dentro de la lección.</summary>
    public int Orden { get; set; }

    /// <summary>Tipo de contenido visual (imagen, animación, código, etc.).</summary>
    public string TipoContenidoVisual { get; set; } = string.Empty;

    /// <summary>Título de la escena.</summary>
    public string? TituloEscena { get; set; }

    /// <summary>Guion ARIA para accesibilidad.</summary>
    public string? GuionAria { get; set; }

    /// <summary>Contenido visual de la escena.</summary>
    public string? ContenidoVisual { get; set; }

    /// <summary>Metadatos adicionales del contenido visual en formato JSON.</summary>
    public string? MetadatosVisuales { get; set; }

    /// <summary>Tipo de transición de entrada (Fade, Slide, etc.).</summary>
    public string TransicionEntrada { get; set; } = "Fade";

    /// <summary>Layout de la escena (AvatarYContenido, SoloContenido, etc.).</summary>
    public string Layout { get; set; } = "AvatarYContenido";

    /// <summary>Duración de la escena en segundos.</summary>
    public int DuracionSegundos { get; set; }

    /// <summary>Indica si la escena incluye una pausa reflexiva.</summary>
    public bool EsPausaReflexiva { get; set; }

    /// <summary>Duración de la pausa reflexiva en segundos.</summary>
    public int SegundosPausa { get; set; }

    /// <summary>Recursos visuales asociados a la escena.</summary>
    public IReadOnlyList<RecursoVisualDto> Recursos { get; set; } = new List<RecursoVisualDto>();
}

/// <summary>
/// Recurso visual asociado a una escena.
/// </summary>
public class RecursoVisualDto
{
    /// <summary>Identificador del recurso.</summary>
    public int Id { get; set; }

    /// <summary>Tipo de recurso (imagen, video, animación, etc.).</summary>
    public string TipoRecurso { get; set; } = string.Empty;

    /// <summary>Nombre del recurso.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>URL del recurso.</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>Texto alternativo para accesibilidad.</summary>
    public string? TextoAlternativo { get; set; }
}
