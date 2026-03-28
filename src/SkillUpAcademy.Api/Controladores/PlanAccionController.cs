using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.DTOs;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Infrastructure.Datos;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador CRUD para planes de acción del usuario.
/// </summary>
[ApiController]
[Route("api/v1/plan-accion")]
[Authorize]
[EnableRateLimiting("general")]
public class PlanAccionController(AppDbContext _contexto) : ControllerBase
{
    /// <summary>Obtiene todos los planes de acción del usuario autenticado.</summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerPlanesAsync()
    {
        Guid usuarioId = ObtenerUsuarioId();

        List<PlanAccionDto> planes = await _contexto.PlanesAccionUsuario
            .Where(p => p.UsuarioId == usuarioId)
            .Include(p => p.Leccion)
            .OrderByDescending(p => p.FechaCreacion)
            .Select(p => new PlanAccionDto
            {
                Id = p.Id,
                LeccionId = p.LeccionId,
                TituloLeccion = p.Leccion.Titulo,
                Compromiso = p.Compromiso,
                ContextoAplicacion = p.ContextoAplicacion,
                FechaObjetivo = p.FechaObjetivo,
                Completado = p.Completado,
                ReflexionResultado = p.ReflexionResultado,
                FechaCreacion = p.FechaCreacion,
                FechaCompletado = p.FechaCompletado
            })
            .ToListAsync();

        return Ok(planes);
    }

    /// <summary>Crea un nuevo plan de acción.</summary>
    [HttpPost]
    public async Task<IActionResult> CrearPlanAsync([FromBody] PeticionCrearPlanAccion peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();

        bool leccionExiste = await _contexto.Lecciones.AnyAsync(l => l.Id == peticion.LeccionId);
        if (!leccionExiste)
        {
            return NotFound(new { error = new { code = "LECCION_NO_ENCONTRADA", message = "La lección no existe." } });
        }

        PlanAccionUsuario plan = new PlanAccionUsuario
        {
            UsuarioId = usuarioId,
            LeccionId = peticion.LeccionId,
            Compromiso = peticion.Compromiso,
            ContextoAplicacion = peticion.ContextoAplicacion,
            FechaObjetivo = peticion.FechaObjetivo,
            FechaCreacion = DateTime.UtcNow
        };

        _contexto.PlanesAccionUsuario.Add(plan);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPlanesAsync), new { }, new PlanAccionDto
        {
            Id = plan.Id,
            LeccionId = plan.LeccionId,
            Compromiso = plan.Compromiso,
            ContextoAplicacion = plan.ContextoAplicacion,
            FechaObjetivo = plan.FechaObjetivo,
            Completado = false,
            FechaCreacion = plan.FechaCreacion
        });
    }

    /// <summary>Marca un plan como completado con reflexión.</summary>
    [HttpPut("{id:int}/completar")]
    public async Task<IActionResult> CompletarPlanAsync(int id, [FromBody] PeticionCompletarPlanAccion peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();

        PlanAccionUsuario? plan = await _contexto.PlanesAccionUsuario
            .FirstOrDefaultAsync(p => p.Id == id && p.UsuarioId == usuarioId);

        if (plan == null)
        {
            return NotFound(new { error = new { code = "PLAN_NO_ENCONTRADO", message = "Plan de acción no encontrado." } });
        }

        plan.Completado = true;
        plan.ReflexionResultado = peticion.ReflexionResultado;
        plan.FechaCompletado = DateTime.UtcNow;

        await _contexto.SaveChangesAsync();

        return Ok(new { mensaje = "Plan de acción completado exitosamente." });
    }

    private Guid ObtenerUsuarioId()
    {
        string? idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(idClaim!);
    }
}
