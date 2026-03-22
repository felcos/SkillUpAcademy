namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Configuración de personalidad y apariencia del avatar Aria.
/// Puede ser global o por empresa (multi-tenant futuro).
/// </summary>
public class ConfiguracionAvatar
{
    public int Id { get; set; }

    /// <summary>Nombre del avatar.</summary>
    public string NombreAvatar { get; set; } = "Aria";

    /// <summary>Slug identificador de esta configuración.</summary>
    public string Slug { get; set; } = "aria-default";

    /// <summary>Descripción de la personalidad.</summary>
    public string? DescripcionPersonalidad { get; set; }

    /// <summary>Tono de comunicación (formal, casual, motivacional, técnico).</summary>
    public string Tono { get; set; } = "profesional";

    /// <summary>Velocidad de habla por defecto (0.75, 1.0, 1.25, 1.5).</summary>
    public decimal VelocidadHabla { get; set; } = 1.0m;

    /// <summary>Nombre de la voz TTS a usar.</summary>
    public string VozTTS { get; set; } = "es-ES-ElviraNeural";

    /// <summary>Prompt base del sistema para la IA (personalidad).</summary>
    public string? PromptSistemaBase { get; set; }

    /// <summary>Estilo visual del avatar (slug del skin: 'corporativa', 'casual', 'tech').</summary>
    public string EstiloVisual { get; set; } = "corporativa";

    /// <summary>Color de fondo del avatar.</summary>
    public string? ColorFondo { get; set; }

    /// <summary>Cómo celebra aciertos.</summary>
    public string EstiloCelebracion { get; set; } = "moderado";

    /// <summary>Cómo maneja errores del usuario.</summary>
    public string EstiloCorreccion { get; set; } = "constructivo";

    /// <summary>Indica si esta es la configuración por defecto.</summary>
    public bool EsDefault { get; set; }

    /// <summary>Indica si está activa.</summary>
    public bool Activo { get; set; } = true;

    /// <summary>Fecha de creación.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}
