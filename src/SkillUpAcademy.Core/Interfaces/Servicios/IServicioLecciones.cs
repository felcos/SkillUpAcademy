using SkillUpAcademy.Core.DTOs.Habilidades;
using SkillUpAcademy.Core.DTOs.Escenas;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio para gestionar lecciones.
/// </summary>
public interface IServicioLecciones
{
    /// <summary>Obtiene el contenido completo de una lección.</summary>
    Task<LeccionDetalleDto> ObtenerLeccionAsync(int leccionId, Guid usuarioId);

    /// <summary>Obtiene las escenas de una lección para el motor del avatar.</summary>
    Task<IReadOnlyList<EscenaDto>> ObtenerEscenasAsync(int leccionId);

    /// <summary>Marca una lección como iniciada.</summary>
    Task IniciarLeccionAsync(int leccionId, Guid usuarioId);

    /// <summary>Marca una lección como completada.</summary>
    Task<ResultadoCompletarLeccion> CompletarLeccionAsync(int leccionId, Guid usuarioId);
}
