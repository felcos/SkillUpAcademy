using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Repositorios;

/// <summary>
/// Repositorio de progreso del usuario.
/// </summary>
public class RepositorioProgreso(AppDbContext _contexto) : IRepositorioProgreso
{
    public async Task<ProgresoUsuario?> ObtenerAsync(Guid usuarioId, int leccionId)
    {
        ProgresoUsuario? progreso = await _contexto.ProgresosUsuario
            .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.LeccionId == leccionId);
        return progreso;
    }

    public async Task<IReadOnlyList<ProgresoUsuario>> ObtenerPorUsuarioAsync(Guid usuarioId)
    {
        List<ProgresoUsuario> progresos = await _contexto.ProgresosUsuario
            .Where(p => p.UsuarioId == usuarioId)
            .Include(p => p.Leccion)
            .ToListAsync();
        return progresos;
    }

    public async Task<IReadOnlyList<ProgresoUsuario>> ObtenerPorUsuarioYAreaAsync(Guid usuarioId, int areaHabilidadId)
    {
        List<ProgresoUsuario> progresos = await _contexto.ProgresosUsuario
            .Where(p => p.UsuarioId == usuarioId)
            .Include(p => p.Leccion)
                .ThenInclude(l => l.Nivel)
            .Where(p => p.Leccion.Nivel.AreaHabilidadId == areaHabilidadId)
            .ToListAsync();
        return progresos;
    }

    public async Task<ProgresoUsuario> CrearOActualizarAsync(ProgresoUsuario progreso)
    {
        ProgresoUsuario? existente = await _contexto.ProgresosUsuario
            .FirstOrDefaultAsync(p => p.UsuarioId == progreso.UsuarioId && p.LeccionId == progreso.LeccionId);

        if (existente == null)
        {
            _contexto.ProgresosUsuario.Add(progreso);
        }
        else
        {
            existente.Estado = progreso.Estado;
            existente.Puntuacion = progreso.Puntuacion;
            existente.Intentos = progreso.Intentos;
            existente.FechaCompletado = progreso.FechaCompletado;
            existente.UltimoAcceso = DateTime.UtcNow;
        }

        await _contexto.SaveChangesAsync();
        return existente ?? progreso;
    }

    public async Task<int> ContarCompletadasPorUsuarioAsync(Guid usuarioId)
    {
        int total = await _contexto.ProgresosUsuario
            .CountAsync(p => p.UsuarioId == usuarioId && p.Estado == EstadoProgreso.Completado);
        return total;
    }

    public async Task<int> ContarCompletadasPorUsuarioYNivelAsync(Guid usuarioId, int nivelId)
    {
        int total = await _contexto.ProgresosUsuario
            .CountAsync(p => p.UsuarioId == usuarioId
                         && p.Estado == EstadoProgreso.Completado
                         && p.Leccion.NivelId == nivelId);
        return total;
    }
}
