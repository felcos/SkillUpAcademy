namespace SkillUpAcademy.Core.DTOs.Habilidades;

/// <summary>
/// Resumen de una lección para listados.
/// </summary>
public class LeccionResumenDto
{
    /// <summary>Identificador de la lección.</summary>
    public int Id { get; set; }

    /// <summary>Título de la lección.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Tipo de lección (teoría, quiz, escenario, etc.).</summary>
    public string TipoLeccion { get; set; } = string.Empty;

    /// <summary>Duración estimada en minutos.</summary>
    public int DuracionMinutos { get; set; }

    /// <summary>Puntos que se obtienen al completar la lección.</summary>
    public int PuntosRecompensa { get; set; }

    /// <summary>Estado actual de la lección para el usuario (no_iniciado, en_progreso, completado).</summary>
    public string Estado { get; set; } = "no_iniciado";

    /// <summary>Puntuación obtenida si la lección fue completada.</summary>
    public int? Puntuacion { get; set; }
}
