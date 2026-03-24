using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.DTOs.Tts;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de administración de proveedores TTS.
/// Permite al admin habilitar/deshabilitar proveedores y configurar API keys.
/// </summary>
public class ServicioAdminTts(
    AppDbContext _contexto,
    ILogger<ServicioAdminTts> _logger) : IServicioAdminTts
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<ConfiguracionProveedorTtsDto>> ObtenerProveedoresAsync()
    {
        List<ProveedorTts> proveedores = await _contexto.ProveedoresTts
            .OrderBy(p => p.Orden)
            .ToListAsync();

        return proveedores.Select(MapearADto).ToList();
    }

    /// <inheritdoc />
    public async Task<ConfiguracionProveedorTtsDto> ObtenerProveedorAsync(TipoProveedorTts tipo)
    {
        ProveedorTts? proveedor = await _contexto.ProveedoresTts
            .FirstOrDefaultAsync(p => p.Tipo == tipo);

        if (proveedor == null)
            throw new ExcepcionNoEncontrado("ProveedorTts", tipo.ToString());

        return MapearADto(proveedor);
    }

    /// <inheritdoc />
    public async Task<ConfiguracionProveedorTtsDto> ActualizarProveedorAsync(TipoProveedorTts tipo, PeticionGuardarProveedorTts peticion)
    {
        ProveedorTts? proveedor = await _contexto.ProveedoresTts
            .FirstOrDefaultAsync(p => p.Tipo == tipo);

        if (proveedor == null)
            throw new ExcepcionNoEncontrado("ProveedorTts", tipo.ToString());

        if (!string.IsNullOrWhiteSpace(peticion.NombreVisible))
            proveedor.NombreVisible = peticion.NombreVisible;

        if (peticion.Descripcion != null)
            proveedor.Descripcion = peticion.Descripcion;

        proveedor.Habilitado = peticion.Habilitado;

        // Solo actualizar API key si se proporciona (para no borrarla accidentalmente)
        if (!string.IsNullOrWhiteSpace(peticion.ApiKey))
            proveedor.ApiKey = peticion.ApiKey;

        if (peticion.Region != null)
            proveedor.Region = peticion.Region;

        if (!string.IsNullOrWhiteSpace(peticion.VozPorDefecto))
            proveedor.VozPorDefecto = peticion.VozPorDefecto;

        if (peticion.Orden.HasValue)
            proveedor.Orden = peticion.Orden.Value;

        proveedor.FechaActualizacion = DateTime.UtcNow;

        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Proveedor TTS {Tipo} actualizado. Habilitado={Habilitado}, TieneKey={TieneKey}",
            tipo, proveedor.Habilitado, !string.IsNullOrWhiteSpace(proveedor.ApiKey));

        return MapearADto(proveedor);
    }

    /// <inheritdoc />
    public async Task<bool> AlternarProveedorAsync(TipoProveedorTts tipo)
    {
        ProveedorTts? proveedor = await _contexto.ProveedoresTts
            .FirstOrDefaultAsync(p => p.Tipo == tipo);

        if (proveedor == null)
            throw new ExcepcionNoEncontrado("ProveedorTts", tipo.ToString());

        proveedor.Habilitado = !proveedor.Habilitado;
        proveedor.FechaActualizacion = DateTime.UtcNow;
        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Proveedor TTS {Tipo} {Estado}", tipo, proveedor.Habilitado ? "habilitado" : "deshabilitado");

        return proveedor.Habilitado;
    }

    private static ConfiguracionProveedorTtsDto MapearADto(ProveedorTts proveedor)
    {
        int cantidadVoces = 0;
        if (!string.IsNullOrWhiteSpace(proveedor.VocesDisponiblesJson))
        {
            try
            {
                List<VozDisponibleDto>? voces = JsonSerializer.Deserialize<List<VozDisponibleDto>>(
                    proveedor.VocesDisponiblesJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                cantidadVoces = voces?.Count ?? 0;
            }
            catch { /* JSON corrupto */ }
        }

        return new ConfiguracionProveedorTtsDto(
            proveedor.Id,
            proveedor.Tipo.ToString(),
            proveedor.NombreVisible,
            proveedor.Descripcion,
            proveedor.Habilitado,
            !string.IsNullOrWhiteSpace(proveedor.ApiKey),
            proveedor.Region,
            proveedor.VozPorDefecto,
            proveedor.Orden,
            cantidadVoces,
            proveedor.FechaCreacion,
            proveedor.FechaActualizacion
        );
    }
}
