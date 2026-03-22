using SkillUpAcademy.Core.DTOs.IA;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de chat con la IA (Aria).
/// </summary>
public interface IServicioChatIA
{
    /// <summary>Inicia una nueva sesión de chat.</summary>
    Task<SesionIADto> IniciarSesionAsync(PeticionIniciarSesionIA peticion, Guid usuarioId);

    /// <summary>Envía un mensaje y recibe la respuesta de Aria.</summary>
    Task<RespuestaMensajeIADto> EnviarMensajeAsync(Guid sesionId, PeticionMensajeIA peticion, Guid usuarioId);

    /// <summary>Obtiene el historial de mensajes de una sesión.</summary>
    Task<IReadOnlyList<MensajeIADto>> ObtenerHistorialAsync(Guid sesionId, Guid usuarioId);

    /// <summary>Cierra una sesión de chat.</summary>
    Task CerrarSesionAsync(Guid sesionId, Guid usuarioId);
}
