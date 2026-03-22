using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Repositorios;

/// <summary>
/// Repositorio de logros.
/// </summary>
public class RepositorioLogros(AppDbContext _contexto) : IRepositorioLogros
{
    public async Task<IReadOnlyList<Logro>> ObtenerTodosAsync()
    {
        List<Logro> logros = await _contexto.Logros.OrderBy(l => l.Id).ToListAsync();
        return logros;
    }

    public async Task<IReadOnlyList<LogroUsuario>> ObtenerLogrosDeUsuarioAsync(Guid usuarioId)
    {
        List<LogroUsuario> logros = await _contexto.LogrosUsuario
            .Where(lu => lu.UsuarioId == usuarioId)
            .Include(lu => lu.Logro)
            .OrderByDescending(lu => lu.FechaDesbloqueo)
            .ToListAsync();
        return logros;
    }

    public async Task<bool> TieneLogroAsync(Guid usuarioId, string slugLogro)
    {
        bool tiene = await _contexto.LogrosUsuario
            .AnyAsync(lu => lu.UsuarioId == usuarioId && lu.Logro.Slug == slugLogro);
        return tiene;
    }

    public async Task<LogroUsuario> DesbloquearAsync(Guid usuarioId, int logroId)
    {
        LogroUsuario nuevoLogro = new LogroUsuario
        {
            UsuarioId = usuarioId,
            LogroId = logroId,
            FechaDesbloqueo = DateTime.UtcNow
        };
        _contexto.LogrosUsuario.Add(nuevoLogro);
        await _contexto.SaveChangesAsync();
        return nuevoLogro;
    }
}
