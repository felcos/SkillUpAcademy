using SkillUpAcademy.Core.DTOs.Admin;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de administración para el panel de control.
/// </summary>
public interface IServicioAdmin
{
    /// <summary>Obtiene el resumen general del panel de administración.</summary>
    Task<ResumenAdmin> ObtenerResumenAsync();

    /// <summary>Obtiene una lista paginada de usuarios.</summary>
    Task<IReadOnlyList<UsuarioAdmin>> ObtenerUsuariosAsync(int pagina, int tamano);

    /// <summary>Obtiene el total de usuarios registrados.</summary>
    Task<int> ObtenerTotalUsuariosAsync();

    /// <summary>Obtiene las estadísticas globales de contenido educativo.</summary>
    Task<EstadisticasContenido> ObtenerEstadisticasContenidoAsync();

    /// <summary>Alterna el estado de bloqueo de IA para un usuario. Devuelve el nuevo estado.</summary>
    Task<bool> AlternarBloqueoIAUsuarioAsync(Guid usuarioId);
}
