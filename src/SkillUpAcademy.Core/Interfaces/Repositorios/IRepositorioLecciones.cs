using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Core.Interfaces.Repositorios;

/// <summary>
/// Repositorio para lecciones y su contenido.
/// </summary>
public interface IRepositorioLecciones
{
    Task<Leccion?> ObtenerPorIdAsync(int id);
    Task<Leccion?> ObtenerConQuizAsync(int id);
    Task<Leccion?> ObtenerConEscenarioAsync(int id);
    Task<Leccion?> ObtenerConEscenasAsync(int id);
    Task<IReadOnlyList<Leccion>> ObtenerPorNivelAsync(int nivelId);
    Task<int> ContarLeccionesActivasAsync();
}
