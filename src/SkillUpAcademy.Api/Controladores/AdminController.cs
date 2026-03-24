using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SkillUpAcademy.Core.DTOs.Admin;
using SkillUpAcademy.Core.DTOs.Tts;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador del panel de administración.
/// </summary>
[ApiController]
[Route("api/v1/admin")]
[Authorize(Roles = "Admin")]
[EnableRateLimiting("general")]
public class AdminController(
    IServicioAdmin _servicioAdmin,
    IServicioAdminTts _servicioAdminTts) : ControllerBase
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

    // ============ TTS ADMIN ============

    /// <summary>Obtiene todos los proveedores TTS configurados.</summary>
    [HttpGet("tts/proveedores")]
    public async Task<IActionResult> ObtenerProveedoresTts()
    {
        IReadOnlyList<ConfiguracionProveedorTtsDto> proveedores = await _servicioAdminTts.ObtenerProveedoresAsync();
        return Ok(proveedores);
    }

    /// <summary>Obtiene un proveedor TTS por tipo.</summary>
    [HttpGet("tts/proveedores/{tipo}")]
    public async Task<IActionResult> ObtenerProveedorTts(string tipo)
    {
        if (!Enum.TryParse<TipoProveedorTts>(tipo, ignoreCase: true, out TipoProveedorTts tipoEnum))
            return BadRequest(new { error = new { message = $"Tipo de proveedor no válido: {tipo}" } });

        ConfiguracionProveedorTtsDto proveedor = await _servicioAdminTts.ObtenerProveedorAsync(tipoEnum);
        return Ok(proveedor);
    }

    /// <summary>Actualiza la configuración de un proveedor TTS.</summary>
    [HttpPut("tts/proveedores/{tipo}")]
    public async Task<IActionResult> ActualizarProveedorTts(string tipo, [FromBody] PeticionGuardarProveedorTts peticion)
    {
        if (!Enum.TryParse<TipoProveedorTts>(tipo, ignoreCase: true, out TipoProveedorTts tipoEnum))
            return BadRequest(new { error = new { message = $"Tipo de proveedor no válido: {tipo}" } });

        ConfiguracionProveedorTtsDto resultado = await _servicioAdminTts.ActualizarProveedorAsync(tipoEnum, peticion);
        return Ok(resultado);
    }

    /// <summary>Alterna el estado habilitado/deshabilitado de un proveedor TTS.</summary>
    [HttpPost("tts/proveedores/{tipo}/alternar")]
    public async Task<IActionResult> AlternarProveedorTts(string tipo)
    {
        if (!Enum.TryParse<TipoProveedorTts>(tipo, ignoreCase: true, out TipoProveedorTts tipoEnum))
            return BadRequest(new { error = new { message = $"Tipo de proveedor no válido: {tipo}" } });

        bool habilitado = await _servicioAdminTts.AlternarProveedorAsync(tipoEnum);
        return Ok(new { habilitado });
    }
}
