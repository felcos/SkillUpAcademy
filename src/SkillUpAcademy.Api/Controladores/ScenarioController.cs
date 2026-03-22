using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUpAcademy.Core.DTOs.Escenario;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de escenarios interactivos.
/// </summary>
[ApiController]
[Route("api/v1/lessons/{leccionId:int}/scenario")]
[Authorize]
public class ScenarioController(IServicioEscenario _servicioEscenario) : ControllerBase
{
    /// <summary>Obtiene un escenario con sus opciones.</summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerEscenario(int leccionId)
    {
        EscenarioDto escenario = await _servicioEscenario.ObtenerEscenarioAsync(leccionId);
        return Ok(escenario);
    }

    /// <summary>Envía la elección del usuario.</summary>
    [HttpPost("choose")]
    public async Task<IActionResult> Elegir(int leccionId, [FromBody] PeticionEleccionEscenario peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        ResultadoEscenarioDto resultado = await _servicioEscenario.ElegirOpcionAsync(leccionId, peticion, usuarioId);
        return Ok(resultado);
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }
}
