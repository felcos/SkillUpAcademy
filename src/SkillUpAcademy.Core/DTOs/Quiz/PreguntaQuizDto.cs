namespace SkillUpAcademy.Core.DTOs.Quiz;

/// <summary>
/// Pregunta de un quiz con sus opciones de respuesta.
/// </summary>
public class PreguntaQuizDto
{
    /// <summary>Identificador de la pregunta.</summary>
    public int Id { get; set; }

    /// <summary>Texto de la pregunta.</summary>
    public string TextoPregunta { get; set; } = string.Empty;

    /// <summary>Orden de la pregunta dentro del quiz.</summary>
    public int Orden { get; set; }

    /// <summary>Lista de opciones disponibles para responder.</summary>
    public IReadOnlyList<OpcionQuizDto> Opciones { get; set; } = new List<OpcionQuizDto>();
}

/// <summary>
/// Opción de respuesta para una pregunta de quiz.
/// </summary>
public class OpcionQuizDto
{
    /// <summary>Identificador de la opción.</summary>
    public int Id { get; set; }

    /// <summary>Texto de la opción de respuesta.</summary>
    public string TextoOpcion { get; set; } = string.Empty;

    /// <summary>Orden de la opción dentro de la pregunta.</summary>
    public int Orden { get; set; }
}
