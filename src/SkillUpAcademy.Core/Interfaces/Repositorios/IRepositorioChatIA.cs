using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Core.Interfaces.Repositorios;

/// <summary>
/// Repositorio para sesiones y mensajes de chat con IA.
/// </summary>
public interface IRepositorioChatIA
{
    Task<SesionChatIA> CrearSesionAsync(SesionChatIA sesion);
    Task<SesionChatIA?> ObtenerSesionAsync(Guid sesionId);
    Task<SesionChatIA?> ObtenerSesionConMensajesAsync(Guid sesionId);
    Task ActualizarSesionAsync(SesionChatIA sesion);
    Task<MensajeChatIA> AgregarMensajeAsync(MensajeChatIA mensaje);
    Task<IReadOnlyList<MensajeChatIA>> ObtenerMensajesAsync(Guid sesionId);
    Task<int> ContarSesionesHoyAsync(Guid usuarioId);
    Task<int> ContarMensajesEnSesionAsync(Guid sesionId);
}
