using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SkillUpAcademy.Core.DTOs.Quiz;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de quizzes.
/// </summary>
[ApiController]
[Route("api/v1/lessons/{leccionId:int}/quiz")]
[Authorize]
[EnableRateLimiting("general")]
public class QuizController(IServicioQuiz _servicioQuiz) : ControllerBase
{
    /// <summary>Obtiene las preguntas del quiz.</summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerPreguntas(int leccionId)
    {
        IReadOnlyList<PreguntaQuizDto> preguntas = await _servicioQuiz.ObtenerPreguntasAsync(leccionId);
        return Ok(preguntas);
    }

    /// <summary>Responde una pregunta individual.</summary>
    [HttpPost("answer")]
    public async Task<IActionResult> Responder(int leccionId, [FromBody] PeticionRespuestaQuiz peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        RespuestaQuizDto resultado = await _servicioQuiz.ResponderPreguntaAsync(leccionId, peticion, usuarioId);
        return Ok(resultado);
    }

    /// <summary>Envía el quiz completo.</summary>
    [HttpPost("submit")]
    public async Task<IActionResult> EnviarCompleto(int leccionId, [FromBody] PeticionQuizCompleto peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        ResultadoQuizDto resultado = await _servicioQuiz.EnviarQuizCompletoAsync(leccionId, peticion, usuarioId);
        return Ok(resultado);
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }
}
