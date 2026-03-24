using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SkillUpAcademy.Core.DTOs.Autenticacion;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de autenticación y gestión de usuarios.
/// </summary>
[ApiController]
[Route("api/v1/auth")]
[EnableRateLimiting("general")]
public class AuthController(IServicioAutenticacion _servicioAuth) : ControllerBase
{
    /// <summary>Registra un nuevo usuario.</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Registrar([FromBody] PeticionRegistro peticion)
    {
        RespuestaLogin resultado = await _servicioAuth.RegistrarAsync(peticion);
        return CreatedAtAction(nameof(ObtenerPerfil), resultado);
    }

    /// <summary>Inicia sesión.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> IniciarSesion([FromBody] PeticionLogin peticion)
    {
        RespuestaLogin resultado = await _servicioAuth.IniciarSesionAsync(peticion);
        return Ok(resultado);
    }

    /// <summary>Renueva el token de acceso.</summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> RenovarToken([FromBody] PeticionRenovarToken peticion)
    {
        RespuestaLogin resultado = await _servicioAuth.RenovarTokenAsync(peticion);
        return Ok(resultado);
    }

    /// <summary>Cierra sesión.</summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> CerrarSesion()
    {
        Guid usuarioId = ObtenerUsuarioId();
        await _servicioAuth.CerrarSesionAsync(usuarioId);
        return NoContent();
    }

    /// <summary>Obtiene el perfil del usuario autenticado.</summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> ObtenerPerfil()
    {
        Guid usuarioId = ObtenerUsuarioId();
        PerfilUsuarioDto perfil = await _servicioAuth.ObtenerPerfilAsync(usuarioId);
        return Ok(perfil);
    }

    /// <summary>Actualiza el perfil del usuario.</summary>
    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> ActualizarPerfil([FromBody] PeticionActualizarPerfil peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        PerfilUsuarioDto perfil = await _servicioAuth.ActualizarPerfilAsync(usuarioId, peticion);
        return Ok(perfil);
    }

    /// <summary>Cambia la contraseña.</summary>
    [HttpPut("me/password")]
    [Authorize]
    public async Task<IActionResult> CambiarContrasena([FromBody] PeticionCambiarContrasena peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        await _servicioAuth.CambiarContrasenaAsync(usuarioId, peticion.ContrasenaActual, peticion.ContrasenaNueva);
        return NoContent();
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }
}

/// <summary>DTO para cambio de contraseña.</summary>
public record PeticionCambiarContrasena(string ContrasenaActual, string ContrasenaNueva);
