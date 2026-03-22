namespace SkillUpAcademy.Core.DTOs.Habilidades;

/// <summary>
/// Detalle completo de una lección.
/// </summary>
public class LeccionDetalleDto
{
    /// <summary>Identificador de la lección.</summary>
    public int Id { get; set; }

    /// <summary>Tipo de lección (teoría, quiz, escenario, etc.).</summary>
    public string TipoLeccion { get; set; } = string.Empty;

    /// <summary>Título de la lección.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Descripción de la lección.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Contenido principal de la lección.</summary>
    public string? Contenido { get; set; }

    /// <summary>Lista de puntos clave de la lección.</summary>
    public IReadOnlyList<string> PuntosClave { get; set; } = new List<string>();

    /// <summary>Guion de audio para la narración.</summary>
    public string? GuionAudio { get; set; }

    /// <summary>Puntos que se obtienen al completar la lección.</summary>
    public int PuntosRecompensa { get; set; }

    /// <summary>Duración estimada en minutos.</summary>
    public int DuracionMinutos { get; set; }

    /// <summary>Estado actual de la lección para el usuario.</summary>
    public string Estado { get; set; } = "no_iniciado";

    /// <summary>Indica si la lección tiene escenas visuales asociadas.</summary>
    public bool TieneEscenas { get; set; }
}

/// <summary>
/// Resultado obtenido al completar una lección.
/// </summary>
public class ResultadoCompletarLeccion
{
    /// <summary>Puntos obtenidos en esta lección.</summary>
    public int PuntosObtenidos { get; set; }

    /// <summary>Puntos totales acumulados del usuario tras completar la lección.</summary>
    public int PuntosTotalesUsuario { get; set; }

    /// <summary>Lista de logros desbloqueados al completar la lección.</summary>
    public IReadOnlyList<string> LogrosDesbloqueados { get; set; } = new List<string>();
}
