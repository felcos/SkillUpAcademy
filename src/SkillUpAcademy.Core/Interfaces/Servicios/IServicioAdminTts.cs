using SkillUpAcademy.Core.DTOs.Tts;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de administración de proveedores TTS.
/// </summary>
public interface IServicioAdminTts
{
    /// <summary>Obtiene todos los proveedores TTS configurados.</summary>
    Task<IReadOnlyList<ConfiguracionProveedorTtsDto>> ObtenerProveedoresAsync();

    /// <summary>Obtiene un proveedor TTS por tipo.</summary>
    Task<ConfiguracionProveedorTtsDto> ObtenerProveedorAsync(TipoProveedorTts tipo);

    /// <summary>Actualiza la configuración de un proveedor TTS.</summary>
    Task<ConfiguracionProveedorTtsDto> ActualizarProveedorAsync(TipoProveedorTts tipo, PeticionGuardarProveedorTts peticion);

    /// <summary>Alterna el estado habilitado/deshabilitado de un proveedor.</summary>
    Task<bool> AlternarProveedorAsync(TipoProveedorTts tipo);
}
