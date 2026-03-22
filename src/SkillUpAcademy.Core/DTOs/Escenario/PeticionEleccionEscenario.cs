using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.Escenario;

/// <summary>
/// Petición con la elección del usuario en un escenario.
/// </summary>
/// <param name="EscenarioId">Identificador del escenario.</param>
/// <param name="OpcionId">Identificador de la opción elegida.</param>
public record PeticionEleccionEscenario(
    [Required] int EscenarioId,
    [Required] int OpcionId
);
