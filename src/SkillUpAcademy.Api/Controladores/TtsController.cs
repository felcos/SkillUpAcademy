using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SkillUpAcademy.Core.DTOs.Tts;
using SkillUpAcademy.Core.Interfaces.Servicios;
using System.Security.Claims;

namespace SkillUpAcademy.Api.Controladores;

/// <summary>
/// Controlador de Text-to-Speech. Sintetiza audio y gestiona preferencias de voz.
/// </summary>
[ApiController]
[Route("api/v1/tts")]
[Authorize]
[EnableRateLimiting("tts")]
public class TtsController(IServicioTts _servicioTts) : ControllerBase
{
    /// <summary>Obtiene las voces disponibles de todos los proveedores habilitados.</summary>
    [HttpGet("voces")]
    public async Task<IActionResult> ObtenerVoces()
    {
        IReadOnlyList<VozDisponibleDto> voces = await _servicioTts.ObtenerVocesDisponiblesAsync();
        return Ok(voces);
    }

    /// <summary>Obtiene la configuración TTS del usuario actual (proveedores, voces, preferencias).</summary>
    [HttpGet("configuracion")]
    public async Task<IActionResult> ObtenerConfiguracion()
    {
        Guid usuarioId = ObtenerUsuarioId();
        ConfiguracionTtsUsuarioDto config = await _servicioTts.ObtenerConfiguracionUsuarioAsync(usuarioId);
        return Ok(config);
    }

    /// <summary>Actualiza las preferencias de voz del usuario.</summary>
    [HttpPut("preferencias")]
    public async Task<IActionResult> ActualizarPreferencias([FromBody] PeticionActualizarVoz peticion)
    {
        Guid usuarioId = ObtenerUsuarioId();
        await _servicioTts.ActualizarPreferenciaVozAsync(usuarioId, peticion);
        ConfiguracionTtsUsuarioDto config = await _servicioTts.ObtenerConfiguracionUsuarioAsync(usuarioId);
        return Ok(config);
    }

    /// <summary>Sintetiza texto a audio MP3.</summary>
    [HttpPost("sintetizar")]
    public async Task<IActionResult> Sintetizar([FromBody] PeticionSintetizar peticion)
    {
        if (string.IsNullOrWhiteSpace(peticion.Texto))
            return BadRequest(new { error = new { message = "El texto es obligatorio." } });

        if (peticion.Texto.Length > 5000)
            return BadRequest(new { error = new { message = "El texto no puede superar los 5000 caracteres." } });

        Guid usuarioId = ObtenerUsuarioId();
        ConfiguracionTtsUsuarioDto config = await _servicioTts.ObtenerConfiguracionUsuarioAsync(usuarioId);

        string? voz = peticion.Voz ?? config.VozSeleccionada;
        decimal velocidad = peticion.Velocidad ?? config.VelocidadVoz;
        string proveedor = config.ProveedorPreferido;

        byte[] audio = await _servicioTts.GenerarAudioAsync(peticion.Texto, voz, velocidad, proveedor);

        if (audio.Length == 0)
            return Ok(new { audioDisponible = false, mensaje = "Usa la voz del navegador (Web Speech API)." });

        return File(audio, "audio/mpeg", "aria-tts.mp3");
    }

    /// <summary>Genera una preview de 5 segundos de una voz específica.</summary>
    [HttpGet("preview/{proveedor}/{idVoz}")]
    public async Task<IActionResult> PreviewVoz(string proveedor, string idVoz)
    {
        byte[] audio = await _servicioTts.GenerarPreviewVozAsync(idVoz, proveedor);

        if (audio.Length == 0)
            return Ok(new { audioDisponible = false, mensaje = "Preview no disponible para este proveedor." });

        return File(audio, "audio/mpeg", "preview.mp3");
    }

    private Guid ObtenerUsuarioId()
    {
        string? claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!);
    }
}
