namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Resultado de una sesión de autoevaluación diagnóstica guiada por Aria.
/// </summary>
public class ResultadoAutoevaluacion
{
    public int Id { get; set; }

    /// <summary>FK al usuario evaluado.</summary>
    public Guid UsuarioId { get; set; }

    /// <summary>FK a la lección de tipo Autoevaluacion.</summary>
    public int LeccionId { get; set; }

    /// <summary>Respuestas en formato JSON: array de {pregunta, respuesta, puntuacion}.</summary>
    public string? RespuestasJson { get; set; }

    /// <summary>Nivel detectado por Aria: "novato", "intermedio", "avanzado".</summary>
    public string NivelDetectado { get; set; } = string.Empty;

    /// <summary>Análisis de Aria sobre fortalezas y debilidades del usuario.</summary>
    public string? ResumenIA { get; set; }

    /// <summary>Fecha de la evaluación.</summary>
    public DateTime FechaEvaluacion { get; set; } = DateTime.UtcNow;

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public Leccion Leccion { get; set; } = null!;
}
