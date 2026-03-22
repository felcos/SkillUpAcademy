using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.IA;

/// <summary>
/// Petición para iniciar una nueva sesión de conversación con IA.
/// </summary>
public class PeticionIniciarSesionIA
{
    /// <summary>Tipo de sesión (coaching, práctica, consulta, etc.).</summary>
    [Required]
    public string TipoSesion { get; set; } = string.Empty;

    /// <summary>Identificador de la lección asociada, si aplica.</summary>
    public int? LeccionId { get; set; }
}
