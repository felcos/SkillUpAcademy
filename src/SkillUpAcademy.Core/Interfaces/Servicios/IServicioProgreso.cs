using SkillUpAcademy.Core.DTOs.Progreso;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de progreso y estadísticas del usuario.
/// </summary>
public interface IServicioProgreso
{
    /// <summary>Obtiene el resumen del dashboard del usuario.</summary>
    Task<DashboardDto> ObtenerDashboardAsync(Guid usuarioId);

    /// <summary>Obtiene el progreso detallado en un área específica.</summary>
    Task<ProgresoAreaDto> ObtenerProgresoAreaAsync(string slugArea, Guid usuarioId);

    /// <summary>Obtiene los logros del usuario.</summary>
    Task<IReadOnlyList<LogroDto>> ObtenerLogrosAsync(Guid usuarioId);

    /// <summary>Verifica y otorga logros pendientes.</summary>
    Task<IReadOnlyList<LogroDto>> VerificarLogrosAsync(Guid usuarioId);

    /// <summary>Actualiza la racha diaria del usuario.</summary>
    Task ActualizarRachaAsync(Guid usuarioId);
}
