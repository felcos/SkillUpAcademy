using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Lección individual dentro de un nivel.
/// </summary>
public class Leccion
{
    public int Id { get; set; }

    /// <summary>FK al nivel.</summary>
    public int NivelId { get; set; }

    /// <summary>Tipo de lección (Teoria, Quiz, Escenario, Roleplay).</summary>
    public TipoLeccion TipoLeccion { get; set; }

    /// <summary>Título de la lección.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Descripción breve.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Contenido principal (Markdown).</summary>
    public string? Contenido { get; set; }

    /// <summary>Puntos clave en formato JSON.</summary>
    public string? PuntosClave { get; set; }

    /// <summary>Guion de audio para TTS de Aria.</summary>
    public string? GuionAudio { get; set; }

    /// <summary>Puntos que se otorgan al completar.</summary>
    public int PuntosRecompensa { get; set; } = 10;

    /// <summary>Orden de la lección dentro del nivel.</summary>
    public int Orden { get; set; }

    /// <summary>Duración estimada en minutos.</summary>
    public int DuracionMinutos { get; set; } = 5;

    /// <summary>Indica si la lección está activa.</summary>
    public bool Activo { get; set; } = true;

    /// <summary>Fecha de creación.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Navegación
    public Nivel Nivel { get; set; } = null!;
    public ICollection<PreguntaQuiz> PreguntasQuiz { get; set; } = new List<PreguntaQuiz>();
    public ICollection<Escenario> Escenarios { get; set; } = new List<Escenario>();
    public ICollection<EscenaLeccion> Escenas { get; set; } = new List<EscenaLeccion>();
    public ICollection<ProgresoUsuario> Progresos { get; set; } = new List<ProgresoUsuario>();
}
