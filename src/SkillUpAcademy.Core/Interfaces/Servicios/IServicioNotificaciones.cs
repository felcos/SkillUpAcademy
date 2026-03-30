using SkillUpAcademy.Core.DTOs.Progreso;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de notificaciones en tiempo real vía SignalR.
/// </summary>
public interface IServicioNotificaciones
{
    /// <summary>Notifica al usuario que desbloqueó un nuevo logro.</summary>
    Task NotificarLogroDesbloqueadoAsync(Guid usuarioId, LogroDto logro);

    /// <summary>Notifica al usuario que completó una lección con sus puntos.</summary>
    Task NotificarLeccionCompletadaAsync(Guid usuarioId, string tituloLeccion, int puntosObtenidos);

    /// <summary>Notifica al usuario que su racha ha aumentado.</summary>
    Task NotificarRachaActualizadaAsync(Guid usuarioId, int nuevaRacha);
}
