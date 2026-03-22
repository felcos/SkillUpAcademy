using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.Quiz;

/// <summary>
/// Petición para enviar todas las respuestas de un quiz completo.
/// </summary>
public class PeticionQuizCompleto
{
    /// <summary>Lista de respuestas del usuario a cada pregunta del quiz.</summary>
    [Required]
    public IReadOnlyList<PeticionRespuestaQuiz> Respuestas { get; set; } = new List<PeticionRespuestaQuiz>();
}
