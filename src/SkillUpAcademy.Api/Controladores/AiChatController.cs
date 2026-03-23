using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUpAcademy.Core.DTOs.IA;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de chat con IA (Aria).
/// </summary>
[ApiController]
[Route("api/v1/ai")]
[Authorize]
public class AiChatController(IServicioChatIA _servicioChatIA) : ControllerBase
{
    /// <summary>Inicia una nueva sesión de chat.</summary>
    [HttpPost("session/start")]
    public async Task<IActionResult> IniciarSesion([FromBody] PeticionIniciarSesionIA peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        SesionIADto sesion = await _servicioChatIA.IniciarSesionAsync(peticion, usuarioId);
        return CreatedAtAction(nameof(ObtenerHistorial), new { sesionId = sesion.Id }, sesion);
    }

    /// <summary>Envía un mensaje al asistente IA (respuesta completa).</summary>
    [HttpPost("session/{sesionId:guid}/message")]
    public async Task<IActionResult> EnviarMensaje(Guid sesionId, [FromBody] PeticionMensajeIA peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        RespuestaMensajeIADto respuesta = await _servicioChatIA.EnviarMensajeAsync(sesionId, peticion, usuarioId);
        return Ok(respuesta);
    }

    /// <summary>Envía un mensaje al asistente IA con respuesta en streaming SSE.</summary>
    [HttpPost("session/{sesionId:guid}/message/stream")]
    public async Task EnviarMensajeStream(Guid sesionId, [FromBody] PeticionMensajeIA peticion, CancellationToken cancellationToken)
    {
        Guid usuarioId = ObtenerUsuarioId();

        Response.ContentType = "text/event-stream";
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");
        Response.Headers.Append("X-Accel-Buffering", "no");

        await foreach (string evento in _servicioChatIA.EnviarMensajeStreamAsync(sesionId, peticion, usuarioId).WithCancellation(cancellationToken))
        {
            await Response.WriteAsync(evento, cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }
    }

    /// <summary>Obtiene el historial de mensajes.</summary>
    [HttpGet("session/{sesionId:guid}/history")]
    public async Task<IActionResult> ObtenerHistorial(Guid sesionId)
    {
        Guid usuarioId = ObtenerUsuarioId();
        IReadOnlyList<MensajeIADto> mensajes = await _servicioChatIA.ObtenerHistorialAsync(sesionId, usuarioId);
        return Ok(mensajes);
    }

    /// <summary>Cierra una sesión de chat.</summary>
    [HttpPost("session/{sesionId:guid}/end")]
    public async Task<IActionResult> CerrarSesion(Guid sesionId)
    {
        Guid usuarioId = ObtenerUsuarioId();
        await _servicioChatIA.CerrarSesionAsync(sesionId, usuarioId);
        return NoContent();
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }
}
