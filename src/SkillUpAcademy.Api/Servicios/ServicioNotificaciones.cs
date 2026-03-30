using Microsoft.AspNetCore.SignalR;
using SkillUpAcademy.Api.Hubs;
using SkillUpAcademy.Core.DTOs.Progreso;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Api.Servicios;

/// <summary>
/// Implementación de notificaciones en tiempo real usando SignalR.
/// Vive en Api porque necesita acceso al tipo concreto del Hub.
/// </summary>
public class ServicioNotificaciones(
    IHubContext<NotificacionesHub> hubContext,
    ILogger<ServicioNotificaciones> logger) : IServicioNotificaciones
{
    /// <inheritdoc />
    public async Task NotificarLogroDesbloqueadoAsync(Guid usuarioId, LogroDto logro)
    {
        try
        {
            await hubContext.Clients.User(usuarioId.ToString()).SendAsync("LogroDesbloqueado", new
            {
                logro.Titulo,
                logro.Icono,
                logro.Descripcion,
                Fecha = DateTime.UtcNow
            });

            logger.LogInformation("Notificación de logro '{Logro}' enviada al usuario {UsuarioId}", logro.Titulo, usuarioId);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "No se pudo enviar notificación de logro al usuario {UsuarioId}", usuarioId);
        }
    }

    /// <inheritdoc />
    public async Task NotificarLeccionCompletadaAsync(Guid usuarioId, string tituloLeccion, int puntosObtenidos)
    {
        try
        {
            await hubContext.Clients.User(usuarioId.ToString()).SendAsync("LeccionCompletada", new
            {
                TituloLeccion = tituloLeccion,
                PuntosObtenidos = puntosObtenidos,
                Fecha = DateTime.UtcNow
            });

            logger.LogInformation("Notificación de lección completada enviada al usuario {UsuarioId}", usuarioId);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "No se pudo enviar notificación de lección al usuario {UsuarioId}", usuarioId);
        }
    }

    /// <inheritdoc />
    public async Task NotificarRachaActualizadaAsync(Guid usuarioId, int nuevaRacha)
    {
        try
        {
            await hubContext.Clients.User(usuarioId.ToString()).SendAsync("RachaActualizada", new
            {
                NuevaRacha = nuevaRacha,
                Fecha = DateTime.UtcNow
            });

            logger.LogInformation("Notificación de racha ({Racha} días) enviada al usuario {UsuarioId}", nuevaRacha, usuarioId);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "No se pudo enviar notificación de racha al usuario {UsuarioId}", usuarioId);
        }
    }
}
