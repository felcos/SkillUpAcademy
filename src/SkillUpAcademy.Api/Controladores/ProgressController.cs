using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUpAcademy.Core.DTOs.Progreso;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de progreso y estadísticas del usuario.
/// </summary>
[ApiController]
[Route("api/v1/progress")]
[Authorize]
public class ProgressController(IServicioProgreso _servicioProgreso) : ControllerBase
{
    /// <summary>Dashboard del usuario.</summary>
    [HttpGet("dashboard")]
    public async Task<IActionResult> ObtenerDashboard()
    {
        Guid usuarioId = ObtenerUsuarioId();
        DashboardDto dashboard = await _servicioProgreso.ObtenerDashboardAsync(usuarioId);
        return Ok(dashboard);
    }

    /// <summary>Progreso en un área específica.</summary>
    [HttpGet("skill/{slug}")]
    public async Task<IActionResult> ObtenerProgresoArea(string slug)
    {
        Guid usuarioId = ObtenerUsuarioId();
        ProgresoAreaDto progreso = await _servicioProgreso.ObtenerProgresoAreaAsync(slug, usuarioId);
        return Ok(progreso);
    }

    /// <summary>Logros del usuario.</summary>
    [HttpGet("achievements")]
    public async Task<IActionResult> ObtenerLogros()
    {
        Guid usuarioId = ObtenerUsuarioId();
        IReadOnlyList<LogroDto> logros = await _servicioProgreso.ObtenerLogrosAsync(usuarioId);
        return Ok(logros);
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }
}
