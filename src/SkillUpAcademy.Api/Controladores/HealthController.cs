using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de health check.
/// </summary>
[ApiController]
[Route("api/v1/health")]
public class HealthController(AppDbContext _contexto) : ControllerBase
{
    /// <summary>Health check básico.</summary>
    [HttpGet]
    public IActionResult Health()
    {
        return Ok(new { estado = "saludable", timestamp = DateTime.UtcNow });
    }

    /// <summary>Readiness check (verifica conexión a BD).</summary>
    [HttpGet("ready")]
    public async Task<IActionResult> Ready()
    {
        try
        {
            bool puedeConectar = await _contexto.Database.CanConnectAsync();
            if (puedeConectar)
            {
                return Ok(new { estado = "listo", baseDeDatos = "conectada", timestamp = DateTime.UtcNow });
            }
            return StatusCode(503, new { estado = "no_listo", baseDeDatos = "desconectada" });
        }
        catch (Exception ex)
        {
            return StatusCode(503, new { estado = "no_listo", error = ex.Message });
        }
    }
}
