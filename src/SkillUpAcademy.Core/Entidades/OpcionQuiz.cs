namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Opción de respuesta de una pregunta de quiz.
/// </summary>
public class OpcionQuiz
{
    public int Id { get; set; }
    public int PreguntaQuizId { get; set; }
    public string TextoOpcion { get; set; } = string.Empty;
    public bool EsCorrecta { get; set; }
    public string? Retroalimentacion { get; set; }
    public int Orden { get; set; }

    // Navegación
    public PreguntaQuiz PreguntaQuiz { get; set; } = null!;
}
