namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Respuesta de un usuario a una pregunta de quiz.
/// </summary>
public class RespuestaQuizUsuario
{
    public int Id { get; set; }
    public Guid UsuarioId { get; set; }
    public int PreguntaQuizId { get; set; }
    public int OpcionSeleccionadaId { get; set; }
    public bool EsCorrecta { get; set; }
    public DateTime FechaRespuesta { get; set; } = DateTime.UtcNow;

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public PreguntaQuiz PreguntaQuiz { get; set; } = null!;
    public OpcionQuiz OpcionSeleccionada { get; set; } = null!;
}
