using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Repositorios;

/// <summary>
/// Repositorio de chat con IA.
/// </summary>
public class RepositorioChatIA(AppDbContext _contexto) : IRepositorioChatIA
{
    public async Task<SesionChatIA> CrearSesionAsync(SesionChatIA sesion)
    {
        _contexto.SesionesChatIA.Add(sesion);
        await _contexto.SaveChangesAsync();
        return sesion;
    }

    public async Task<SesionChatIA?> ObtenerSesionAsync(Guid sesionId)
    {
        SesionChatIA? sesion = await _contexto.SesionesChatIA
            .FirstOrDefaultAsync(s => s.Id == sesionId);
        return sesion;
    }

    public async Task<SesionChatIA?> ObtenerSesionConMensajesAsync(Guid sesionId)
    {
        SesionChatIA? sesion = await _contexto.SesionesChatIA
            .Include(s => s.Mensajes.OrderBy(m => m.FechaEnvio))
            .FirstOrDefaultAsync(s => s.Id == sesionId);
        return sesion;
    }

    public async Task ActualizarSesionAsync(SesionChatIA sesion)
    {
        _contexto.SesionesChatIA.Update(sesion);
        await _contexto.SaveChangesAsync();
    }

    public async Task<MensajeChatIA> AgregarMensajeAsync(MensajeChatIA mensaje)
    {
        _contexto.MensajesChatIA.Add(mensaje);
        await _contexto.SaveChangesAsync();
        return mensaje;
    }

    public async Task<IReadOnlyList<MensajeChatIA>> ObtenerMensajesAsync(Guid sesionId)
    {
        List<MensajeChatIA> mensajes = await _contexto.MensajesChatIA
            .Where(m => m.SesionId == sesionId)
            .OrderBy(m => m.FechaEnvio)
            .ToListAsync();
        return mensajes;
    }

    public async Task<int> ContarSesionesHoyAsync(Guid usuarioId)
    {
        DateTime hoy = DateTime.UtcNow.Date;
        int total = await _contexto.SesionesChatIA
            .CountAsync(s => s.UsuarioId == usuarioId && s.FechaInicio >= hoy);
        return total;
    }

    public async Task<int> ContarMensajesEnSesionAsync(Guid sesionId)
    {
        int total = await _contexto.MensajesChatIA
            .CountAsync(m => m.SesionId == sesionId);
        return total;
    }
}
