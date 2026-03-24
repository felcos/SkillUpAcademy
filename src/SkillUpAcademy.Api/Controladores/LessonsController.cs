using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SkillUpAcademy.Core.DTOs.Habilidades;
using SkillUpAcademy.Core.DTOs.Escenas;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de lecciones.
/// </summary>
[ApiController]
[Route("api/v1/lessons")]
[Authorize]
[EnableRateLimiting("general")]
public class LessonsController(IServicioLecciones _servicioLecciones) : ControllerBase
{
    /// <summary>Obtiene el contenido completo de una lección.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerLeccion(int id)
    {
        Guid usuarioId = ObtenerUsuarioId();
        LeccionDetalleDto leccion = await _servicioLecciones.ObtenerLeccionAsync(id, usuarioId);
        return Ok(leccion);
    }

    /// <summary>Obtiene las escenas de una lección para el motor del avatar.</summary>
    [HttpGet("{id:int}/scenes")]
    public async Task<IActionResult> ObtenerEscenas(int id)
    {
        IReadOnlyList<EscenaDto> escenas = await _servicioLecciones.ObtenerEscenasAsync(id);
        return Ok(escenas);
    }

    /// <summary>Marca una lección como iniciada.</summary>
    [HttpPost("{id:int}/start")]
    public async Task<IActionResult> IniciarLeccion(int id)
    {
        Guid usuarioId = ObtenerUsuarioId();
        await _servicioLecciones.IniciarLeccionAsync(id, usuarioId);
        return Ok(new { mensaje = "Lección iniciada" });
    }

    /// <summary>Marca una lección como completada.</summary>
    [HttpPost("{id:int}/complete")]
    public async Task<IActionResult> CompletarLeccion(int id)
    {
        Guid usuarioId = ObtenerUsuarioId();
        ResultadoCompletarLeccion resultado = await _servicioLecciones.CompletarLeccionAsync(id, usuarioId);
        return Ok(resultado);
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }
}
