namespace SkillUpAcademy.Core.DTOs.Escenas;

/// <summary>
/// Petición para actualizar parcialmente una escena.
/// </summary>
public class PeticionActualizarEscena
{
    /// <summary>Nuevo tipo de contenido visual.</summary>
    public string? TipoContenidoVisual { get; set; }

    /// <summary>Nuevo título de la escena.</summary>
    public string? TituloEscena { get; set; }

    /// <summary>Nuevo guion ARIA.</summary>
    public string? GuionAria { get; set; }

    /// <summary>Nuevo contenido visual.</summary>
    public string? ContenidoVisual { get; set; }

    /// <summary>Nuevos metadatos visuales.</summary>
    public string? MetadatosVisuales { get; set; }

    /// <summary>Nueva transición de entrada.</summary>
    public string? TransicionEntrada { get; set; }

    /// <summary>Nuevo layout.</summary>
    public string? Layout { get; set; }

    /// <summary>Nueva duración en segundos.</summary>
    public int? DuracionSegundos { get; set; }

    /// <summary>Indica si es pausa reflexiva.</summary>
    public bool? EsPausaReflexiva { get; set; }

    /// <summary>Nuevos segundos de pausa.</summary>
    public int? SegundosPausa { get; set; }
}
