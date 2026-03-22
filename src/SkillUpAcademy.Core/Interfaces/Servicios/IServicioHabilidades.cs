using SkillUpAcademy.Core.DTOs.Habilidades;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio para gestionar áreas de habilidad y niveles.
/// </summary>
public interface IServicioHabilidades
{
    /// <summary>Lista todas las áreas de habilidad con progreso del usuario.</summary>
    Task<IReadOnlyList<AreaHabilidadDto>> ObtenerAreasAsync(Guid? usuarioId = null);

    /// <summary>Obtiene el detalle de un área por su slug.</summary>
    Task<AreaHabilidadDetalleDto> ObtenerAreaPorSlugAsync(string slug, Guid? usuarioId = null);

    /// <summary>Lista los niveles de un área con sus lecciones.</summary>
    Task<IReadOnlyList<NivelDto>> ObtenerNivelesAsync(string slugArea, Guid usuarioId);

    /// <summary>Obtiene el detalle de un nivel con sus lecciones y progreso.</summary>
    Task<NivelDetalleDto> ObtenerNivelAsync(string slugArea, int numeroNivel, Guid usuarioId);
}
