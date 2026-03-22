using SkillUpAcademy.Core.DTOs.Quiz;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio para gestionar quizzes.
/// </summary>
public interface IServicioQuiz
{
    /// <summary>Obtiene las preguntas de un quiz sin las respuestas correctas.</summary>
    Task<IReadOnlyList<PreguntaQuizDto>> ObtenerPreguntasAsync(int leccionId);

    /// <summary>Envía la respuesta a una pregunta y devuelve feedback.</summary>
    Task<RespuestaQuizDto> ResponderPreguntaAsync(int leccionId, PeticionRespuestaQuiz peticion, Guid usuarioId);

    /// <summary>Envía el quiz completo y devuelve el resultado.</summary>
    Task<ResultadoQuizDto> EnviarQuizCompletoAsync(int leccionId, PeticionQuizCompleto peticion, Guid usuarioId);
}
