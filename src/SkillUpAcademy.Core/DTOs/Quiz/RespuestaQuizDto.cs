namespace SkillUpAcademy.Core.DTOs.Quiz;

/// <summary>
/// Resultado de una respuesta individual a una pregunta de quiz.
/// </summary>
public class RespuestaQuizDto
{
    /// <summary>Indica si la respuesta fue correcta.</summary>
    public bool EsCorrecta { get; set; }

    /// <summary>Retroalimentación sobre la respuesta.</summary>
    public string? Retroalimentacion { get; set; }

    /// <summary>Identificador de la opción correcta.</summary>
    public int OpcionCorrectaId { get; set; }

    /// <summary>Explicación detallada de por qué es la respuesta correcta.</summary>
    public string? Explicacion { get; set; }
}
