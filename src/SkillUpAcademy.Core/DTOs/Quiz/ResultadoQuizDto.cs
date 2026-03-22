namespace SkillUpAcademy.Core.DTOs.Quiz;

/// <summary>
/// Resultado final de un quiz completado.
/// </summary>
public class ResultadoQuizDto
{
    /// <summary>Cantidad total de preguntas en el quiz.</summary>
    public int PreguntasTotales { get; set; }

    /// <summary>Cantidad de respuestas correctas.</summary>
    public int RespuestasCorrectas { get; set; }

    /// <summary>Puntuación obtenida (0-100).</summary>
    public int Puntuacion { get; set; }

    /// <summary>Puntos de recompensa obtenidos.</summary>
    public int PuntosObtenidos { get; set; }

    /// <summary>Indica si el usuario aprobó el quiz.</summary>
    public bool Aprobado { get; set; }

    /// <summary>Puntuación mínima requerida para aprobar.</summary>
    public int PuntuacionMinima { get; set; } = 60;
}
