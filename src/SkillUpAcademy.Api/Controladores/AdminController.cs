using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SkillUpAcademy.Core.DTOs.Admin;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador del panel de administración.
/// </summary>
[ApiController]
[Route("api/v1/admin")]
[Authorize(Roles = "Admin")]
[EnableRateLimiting("general")]
public class AdminController(IServicioAdmin _servicioAdmin) : ControllerBase
{
    /// <summary>Obtiene el resumen general del panel de administración.</summary>
    [HttpGet("resumen")]
    public async Task<IActionResult> ObtenerResumen()
    {
        ResumenAdmin resumen = await _servicioAdmin.ObtenerResumenAsync();
        return Ok(resumen);
    }

    /// <summary>Obtiene una lista paginada de usuarios.</summary>
    [HttpGet("usuarios")]
    public async Task<IActionResult> ObtenerUsuarios([FromQuery] int pagina = 1, [FromQuery] int tamano = 20)
    {
        IReadOnlyList<UsuarioAdmin> usuarios = await _servicioAdmin.ObtenerUsuariosAsync(pagina, tamano);
        int total = await _servicioAdmin.ObtenerTotalUsuariosAsync();
        return Ok(new { usuarios, total, pagina, tamano });
    }

    /// <summary>Obtiene las estadísticas globales del contenido educativo.</summary>
    [HttpGet("estadisticas-contenido")]
    public async Task<IActionResult> ObtenerEstadisticasContenido()
    {
        EstadisticasContenido estadisticas = await _servicioAdmin.ObtenerEstadisticasContenidoAsync();
        return Ok(estadisticas);
    }

    /// <summary>Alterna el bloqueo de IA para un usuario.</summary>
    [HttpPost("usuarios/{id}/alternar-bloqueo-ia")]
    public async Task<IActionResult> AlternarBloqueoIA(Guid id)
    {
        bool nuevoEstado = await _servicioAdmin.AlternarBloqueoIAUsuarioAsync(id);
        return Ok(new { estaBloqueadoIA = nuevoEstado });
    }
}
