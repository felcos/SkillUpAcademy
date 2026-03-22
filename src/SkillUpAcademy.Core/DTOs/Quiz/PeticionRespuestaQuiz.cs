using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.Quiz;

/// <summary>
/// Petición con la respuesta del usuario a una pregunta de quiz.
/// </summary>
/// <param name="PreguntaId">Identificador de la pregunta respondida.</param>
/// <param name="OpcionSeleccionadaId">Identificador de la opción seleccionada.</param>
public record PeticionRespuestaQuiz(
    [Required] int PreguntaId,
    [Required] int OpcionSeleccionadaId
);
