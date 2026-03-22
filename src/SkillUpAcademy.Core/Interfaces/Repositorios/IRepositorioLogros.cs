using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Core.Interfaces.Repositorios;

/// <summary>
/// Repositorio para logros y achievements.
/// </summary>
public interface IRepositorioLogros
{
    Task<IReadOnlyList<Logro>> ObtenerTodosAsync();
    Task<IReadOnlyList<LogroUsuario>> ObtenerLogrosDeUsuarioAsync(Guid usuarioId);
    Task<bool> TieneLogroAsync(Guid usuarioId, string slugLogro);
    Task<LogroUsuario> DesbloquearAsync(Guid usuarioId, int logroId);
}
