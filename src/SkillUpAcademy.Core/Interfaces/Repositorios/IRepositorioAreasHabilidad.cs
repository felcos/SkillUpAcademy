using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Core.Interfaces.Repositorios;

/// <summary>
/// Repositorio para áreas de habilidad.
/// </summary>
public interface IRepositorioAreasHabilidad
{
    Task<IReadOnlyList<AreaHabilidad>> ObtenerTodasAsync();
    Task<AreaHabilidad?> ObtenerPorSlugAsync(string slug);
    Task<AreaHabilidad?> ObtenerPorIdAsync(int id);
    Task<AreaHabilidad?> ObtenerConNivelesYLeccionesAsync(string slug);
}
