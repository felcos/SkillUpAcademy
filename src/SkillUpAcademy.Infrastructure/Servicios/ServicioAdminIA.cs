using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.DTOs.IA;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de administración de proveedores de IA.
/// Permite al admin configurar y gestionar proveedores (Anthropic, OpenAI, Groq, etc.).
/// </summary>
public class ServicioAdminIA(
    AppDbContext _contexto,
    ILogger<ServicioAdminIA> _logger) : IServicioAdminIA
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<ConfiguracionProveedorIADto>> ObtenerProveedoresAsync()
    {
        List<ProveedorIA> proveedores = await _contexto.ProveedoresIA
            .OrderBy(p => p.Tipo)
            .ToListAsync();

        return proveedores.Select(MapearADto).ToList();
    }

    /// <inheritdoc />
    public async Task<ConfiguracionProveedorIADto> ObtenerProveedorAsync(TipoProveedorIA tipo)
    {
        ProveedorIA? proveedor = await _contexto.ProveedoresIA
            .FirstOrDefaultAsync(p => p.Tipo == tipo);

        if (proveedor == null)
            throw new ExcepcionNoEncontrado("ProveedorIA", tipo.ToString());

        return MapearADto(proveedor);
    }

    /// <inheritdoc />
    public async Task<ConfiguracionProveedorIADto> ActualizarProveedorAsync(TipoProveedorIA tipo, PeticionGuardarProveedorIA peticion)
    {
        ProveedorIA? proveedor = await _contexto.ProveedoresIA
            .FirstOrDefaultAsync(p => p.Tipo == tipo);

        if (proveedor == null)
            throw new ExcepcionNoEncontrado("ProveedorIA", tipo.ToString());

        if (!string.IsNullOrWhiteSpace(peticion.NombreVisible))
            proveedor.NombreVisible = peticion.NombreVisible;

        if (peticion.Descripcion != null)
            proveedor.Descripcion = peticion.Descripcion;

        proveedor.Habilitado = peticion.Habilitado;

        if (!string.IsNullOrWhiteSpace(peticion.ApiKey))
            proveedor.ApiKey = peticion.ApiKey;

        if (!string.IsNullOrWhiteSpace(peticion.UrlBase))
            proveedor.UrlBase = peticion.UrlBase;

        if (!string.IsNullOrWhiteSpace(peticion.ModeloChat))
            proveedor.ModeloChat = peticion.ModeloChat;

        if (peticion.MaxTokens > 0)
            proveedor.MaxTokens = peticion.MaxTokens;

        if (peticion.Temperatura >= 0 && peticion.Temperatura <= 2)
            proveedor.Temperatura = peticion.Temperatura;

        proveedor.FechaModificacion = DateTime.UtcNow;

        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Proveedor IA {Tipo} actualizado. Habilitado={Habilitado}, Modelo={Modelo}",
            tipo, proveedor.Habilitado, proveedor.ModeloChat);

        return MapearADto(proveedor);
    }

    /// <inheritdoc />
    public async Task<bool> AlternarProveedorAsync(TipoProveedorIA tipo)
    {
        ProveedorIA? proveedor = await _contexto.ProveedoresIA
            .FirstOrDefaultAsync(p => p.Tipo == tipo);

        if (proveedor == null)
            throw new ExcepcionNoEncontrado("ProveedorIA", tipo.ToString());

        proveedor.Habilitado = !proveedor.Habilitado;
        proveedor.FechaModificacion = DateTime.UtcNow;
        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Proveedor IA {Tipo} {Estado}", tipo, proveedor.Habilitado ? "habilitado" : "deshabilitado");

        return proveedor.Habilitado;
    }

    /// <inheritdoc />
    public async Task<ConfiguracionProveedorIADto> EstablecerActivoAsync(TipoProveedorIA tipo)
    {
        ProveedorIA? proveedor = await _contexto.ProveedoresIA
            .FirstOrDefaultAsync(p => p.Tipo == tipo);

        if (proveedor == null)
            throw new ExcepcionNoEncontrado("ProveedorIA", tipo.ToString());

        if (!proveedor.Habilitado)
            throw new InvalidOperationException($"El proveedor {tipo} debe estar habilitado para ser activado.");

        // Desactivar todos los demás
        List<ProveedorIA> todos = await _contexto.ProveedoresIA.ToListAsync();
        foreach (ProveedorIA p in todos)
        {
            p.EsActivo = p.Tipo == tipo;
            p.FechaModificacion = DateTime.UtcNow;
        }

        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Proveedor IA {Tipo} establecido como activo", tipo);

        return MapearADto(proveedor);
    }

    private static ConfiguracionProveedorIADto MapearADto(ProveedorIA proveedor)
    {
        return new ConfiguracionProveedorIADto
        {
            Id = proveedor.Id,
            Tipo = proveedor.Tipo.ToString(),
            NombreVisible = proveedor.NombreVisible,
            Descripcion = proveedor.Descripcion,
            Habilitado = proveedor.Habilitado,
            EsActivo = proveedor.EsActivo,
            TieneApiKey = !string.IsNullOrWhiteSpace(proveedor.ApiKey),
            UrlBase = proveedor.UrlBase,
            ModeloChat = proveedor.ModeloChat,
            MaxTokens = proveedor.MaxTokens,
            Temperatura = proveedor.Temperatura
        };
    }
}
