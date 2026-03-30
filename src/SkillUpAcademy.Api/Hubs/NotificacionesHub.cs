using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SkillUpAcademy.Api.Hubs;

/// <summary>
/// Hub de SignalR para notificaciones en tiempo real.
/// Los clientes se conectan automáticamente al autenticarse y reciben eventos push.
/// </summary>
[Authorize]
public class NotificacionesHub : Hub
{
    private readonly ILogger<NotificacionesHub> _logger;

    /// <summary>
    /// Constructor del hub de notificaciones.
    /// </summary>
    public NotificacionesHub(ILogger<NotificacionesHub> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task OnConnectedAsync()
    {
        string? usuarioId = Context.UserIdentifier;
        _logger.LogInformation("Usuario {UsuarioId} conectado a NotificacionesHub", usuarioId);
        await base.OnConnectedAsync();
    }

    /// <inheritdoc />
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? usuarioId = Context.UserIdentifier;
        _logger.LogInformation("Usuario {UsuarioId} desconectado de NotificacionesHub", usuarioId);
        await base.OnDisconnectedAsync(exception);
    }
}
