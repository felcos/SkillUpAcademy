using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Repositorios;

/// <summary>
/// Repositorio de lecciones.
/// </summary>
public class RepositorioLecciones(AppDbContext _contexto) : IRepositorioLecciones
{
    public async Task<Leccion?> ObtenerPorIdAsync(int id)
    {
        Leccion? leccion = await _contexto.Lecciones
            .FirstOrDefaultAsync(l => l.Id == id && l.Activo);
        return leccion;
    }

    public async Task<Leccion?> ObtenerConQuizAsync(int id)
    {
        Leccion? leccion = await _contexto.Lecciones
            .Include(l => l.PreguntasQuiz.OrderBy(p => p.Orden))
                .ThenInclude(p => p.Opciones.OrderBy(o => o.Orden))
            .FirstOrDefaultAsync(l => l.Id == id && l.Activo);
        return leccion;
    }

    public async Task<Leccion?> ObtenerConEscenarioAsync(int id)
    {
        Leccion? leccion = await _contexto.Lecciones
            .Include(l => l.Escenarios)
                .ThenInclude(e => e.Opciones.OrderBy(o => o.Orden))
            .FirstOrDefaultAsync(l => l.Id == id && l.Activo);
        return leccion;
    }

    public async Task<Leccion?> ObtenerConEscenasAsync(int id)
    {
        Leccion? leccion = await _contexto.Lecciones
            .Include(l => l.Escenas.OrderBy(e => e.Orden))
                .ThenInclude(e => e.Recursos)
            .FirstOrDefaultAsync(l => l.Id == id && l.Activo);
        return leccion;
    }

    public async Task<IReadOnlyList<Leccion>> ObtenerPorNivelAsync(int nivelId)
    {
        List<Leccion> lecciones = await _contexto.Lecciones
            .Where(l => l.NivelId == nivelId && l.Activo)
            .OrderBy(l => l.Orden)
            .ToListAsync();
        return lecciones;
    }

    public async Task<int> ContarLeccionesActivasAsync()
    {
        int total = await _contexto.Lecciones.CountAsync(l => l.Activo);
        return total;
    }
}
