using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Repositorios;

/// <summary>
/// Repositorio de áreas de habilidad.
/// </summary>
public class RepositorioAreasHabilidad(AppDbContext _contexto) : IRepositorioAreasHabilidad
{
    public async Task<IReadOnlyList<AreaHabilidad>> ObtenerTodasAsync()
    {
        List<AreaHabilidad> areas = await _contexto.AreasHabilidad
            .Where(a => a.Activo)
            .OrderBy(a => a.Orden)
            .ToListAsync();
        return areas;
    }

    public async Task<AreaHabilidad?> ObtenerPorSlugAsync(string slug)
    {
        AreaHabilidad? area = await _contexto.AreasHabilidad
            .FirstOrDefaultAsync(a => a.Slug == slug && a.Activo);
        return area;
    }

    public async Task<AreaHabilidad?> ObtenerPorIdAsync(int id)
    {
        AreaHabilidad? area = await _contexto.AreasHabilidad
            .FirstOrDefaultAsync(a => a.Id == id && a.Activo);
        return area;
    }

    public async Task<AreaHabilidad?> ObtenerConNivelesYLeccionesAsync(string slug)
    {
        AreaHabilidad? area = await _contexto.AreasHabilidad
            .Include(a => a.Niveles.Where(n => n.Activo).OrderBy(n => n.NumeroNivel))
                .ThenInclude(n => n.Lecciones.Where(l => l.Activo).OrderBy(l => l.Orden))
            .FirstOrDefaultAsync(a => a.Slug == slug && a.Activo);
        return area;
    }
}
