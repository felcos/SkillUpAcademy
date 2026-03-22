using SkillUpAcademy.Core.DTOs.Escenas;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio para gestionar el motor de escenas del avatar.
/// </summary>
public interface IServicioEscenas
{
    /// <summary>Obtiene las escenas de una lección ordenadas.</summary>
    Task<IReadOnlyList<EscenaDto>> ObtenerEscenasDeLeccionAsync(int leccionId);

    /// <summary>Genera automáticamente escenas para una lección a partir de su contenido.</summary>
    Task<IReadOnlyList<EscenaDto>> GenerarEscenasAutomaticasAsync(int leccionId);

    /// <summary>Actualiza una escena existente.</summary>
    Task<EscenaDto> ActualizarEscenaAsync(int escenaId, PeticionActualizarEscena peticion);

    /// <summary>Reordena las escenas de una lección.</summary>
    Task ReordenarEscenasAsync(int leccionId, IReadOnlyList<int> ordenEscenaIds);
}
