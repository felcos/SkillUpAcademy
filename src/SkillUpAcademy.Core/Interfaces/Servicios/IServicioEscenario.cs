using SkillUpAcademy.Core.DTOs.Escenario;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio para gestionar escenarios interactivos.
/// </summary>
public interface IServicioEscenario
{
    /// <summary>Obtiene un escenario con su situación y opciones.</summary>
    Task<EscenarioDto> ObtenerEscenarioAsync(int leccionId);

    /// <summary>Envía la elección del usuario en un escenario.</summary>
    Task<ResultadoEscenarioDto> ElegirOpcionAsync(int leccionId, PeticionEleccionEscenario peticion, Guid usuarioId);
}
