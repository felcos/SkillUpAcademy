using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Escena individual dentro de una lección. Define lo que el avatar muestra y dice.
/// </summary>
public class EscenaLeccion
{
    public int Id { get; set; }

    /// <summary>FK a la lección.</summary>
    public int LeccionId { get; set; }

    /// <summary>Orden de la escena dentro de la lección.</summary>
    public int Orden { get; set; }

    /// <summary>Tipo de contenido visual de esta escena.</summary>
    public TipoContenidoVisual TipoContenidoVisual { get; set; }

    /// <summary>Título breve de la escena (para el mini-mapa).</summary>
    public string? TituloEscena { get; set; }

    /// <summary>Lo que dice Aria en esta escena (texto para TTS).</summary>
    public string? GuionAria { get; set; }

    /// <summary>Contenido visual: puede ser texto, URL de imagen, datos de diagrama, etc.</summary>
    public string? ContenidoVisual { get; set; }

    /// <summary>Datos extra del visual en JSON (posiciones, animaciones, colores).</summary>
    public string? MetadatosVisuales { get; set; }

    /// <summary>Tipo de transición de entrada.</summary>
    public TipoTransicion TransicionEntrada { get; set; } = TipoTransicion.Fade;

    /// <summary>Layout de la escena (avatar+contenido, solo avatar, solo contenido, PiP).</summary>
    public TipoLayout Layout { get; set; } = TipoLayout.AvatarYContenido;

    /// <summary>Duración estimada en segundos.</summary>
    public int DuracionSegundos { get; set; } = 15;

    /// <summary>Indica si es una pausa reflexiva (timer visible).</summary>
    public bool EsPausaReflexiva { get; set; }

    /// <summary>Segundos de pausa reflexiva.</summary>
    public int SegundosPausa { get; set; }

    // Navegación
    public Leccion Leccion { get; set; } = null!;
    public ICollection<RecursoVisual> Recursos { get; set; } = new List<RecursoVisual>();
}
