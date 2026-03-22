namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Pregunta de un quiz.
/// </summary>
public class PreguntaQuiz
{
    public int Id { get; set; }
    public int LeccionId { get; set; }
    public string TextoPregunta { get; set; } = string.Empty;
    public string? Explicacion { get; set; }
    public int Orden { get; set; }

    // Navegación
    public Leccion Leccion { get; set; } = null!;
    public ICollection<OpcionQuiz> Opciones { get; set; } = new List<OpcionQuiz>();
}
