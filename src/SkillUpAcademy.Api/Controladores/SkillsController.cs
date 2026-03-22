using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUpAcademy.Core.DTOs.Habilidades;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de áreas de habilidad y niveles.
/// </summary>
[ApiController]
[Route("api/v1/skills")]
public class SkillsController(IServicioHabilidades _servicioHabilidades) : ControllerBase
{
    /// <summary>Lista todas las áreas de habilidad.</summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerAreas()
    {
        Guid? usuarioId = ObtenerUsuarioIdOpcional();
        IReadOnlyList<AreaHabilidadDto> areas = await _servicioHabilidades.ObtenerAreasAsync(usuarioId);
        return Ok(areas);
    }

    /// <summary>Obtiene el detalle de un área por slug.</summary>
    [HttpGet("{slug}")]
    public async Task<IActionResult> ObtenerArea(string slug)
    {
        Guid? usuarioId = ObtenerUsuarioIdOpcional();
        AreaHabilidadDetalleDto area = await _servicioHabilidades.ObtenerAreaPorSlugAsync(slug, usuarioId);
        return Ok(area);
    }

    /// <summary>Lista los niveles de un área.</summary>
    [HttpGet("{slug}/levels")]
    [Authorize]
    public async Task<IActionResult> ObtenerNiveles(string slug)
    {
        Guid usuarioId = ObtenerUsuarioId();
        IReadOnlyList<NivelDto> niveles = await _servicioHabilidades.ObtenerNivelesAsync(slug, usuarioId);
        return Ok(niveles);
    }

    /// <summary>Obtiene el detalle de un nivel.</summary>
    [HttpGet("{slug}/levels/{numeroNivel:int}")]
    [Authorize]
    public async Task<IActionResult> ObtenerNivel(string slug, int numeroNivel)
    {
        Guid usuarioId = ObtenerUsuarioId();
        NivelDetalleDto nivel = await _servicioHabilidades.ObtenerNivelAsync(slug, numeroNivel, usuarioId);
        return Ok(nivel);
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }

    private Guid? ObtenerUsuarioIdOpcional()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return claim != null ? Guid.Parse(claim) : null;
    }
}
