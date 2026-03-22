using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Core.Interfaces.Repositorios;

/// <summary>
/// Repositorio para el progreso del usuario.
/// </summary>
public interface IRepositorioProgreso
{
    Task<ProgresoUsuario?> ObtenerAsync(Guid usuarioId, int leccionId);
    Task<IReadOnlyList<ProgresoUsuario>> ObtenerPorUsuarioAsync(Guid usuarioId);
    Task<IReadOnlyList<ProgresoUsuario>> ObtenerPorUsuarioYAreaAsync(Guid usuarioId, int areaHabilidadId);
    Task<ProgresoUsuario> CrearOActualizarAsync(ProgresoUsuario progreso);
    Task<int> ContarCompletadasPorUsuarioAsync(Guid usuarioId);
    Task<int> ContarCompletadasPorUsuarioYNivelAsync(Guid usuarioId, int nivelId);
}
