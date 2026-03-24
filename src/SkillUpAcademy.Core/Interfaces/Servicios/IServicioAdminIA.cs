using SkillUpAcademy.Core.DTOs.IA;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de administración de proveedores de IA.
/// </summary>
public interface IServicioAdminIA
{
    /// <summary>Obtiene todos los proveedores de IA configurados.</summary>
    Task<IReadOnlyList<ConfiguracionProveedorIADto>> ObtenerProveedoresAsync();

    /// <summary>Obtiene un proveedor de IA por tipo.</summary>
    Task<ConfiguracionProveedorIADto> ObtenerProveedorAsync(TipoProveedorIA tipo);

    /// <summary>Actualiza la configuración de un proveedor de IA.</summary>
    Task<ConfiguracionProveedorIADto> ActualizarProveedorAsync(TipoProveedorIA tipo, PeticionGuardarProveedorIA peticion);

    /// <summary>Alterna el estado habilitado/deshabilitado de un proveedor.</summary>
    Task<bool> AlternarProveedorAsync(TipoProveedorIA tipo);

    /// <summary>Establece un proveedor como el activo para conversaciones.</summary>
    Task<ConfiguracionProveedorIADto> EstablecerActivoAsync(TipoProveedorIA tipo);
}
