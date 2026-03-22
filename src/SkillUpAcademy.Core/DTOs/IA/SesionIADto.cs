namespace SkillUpAcademy.Core.DTOs.IA;

/// <summary>
/// Datos de una sesión de conversación con IA.
/// </summary>
public class SesionIADto
{
    /// <summary>Identificador único de la sesión.</summary>
    public Guid Id { get; set; }

    /// <summary>Tipo de sesión (coaching, práctica, consulta, etc.).</summary>
    public string TipoSesion { get; set; } = string.Empty;

    /// <summary>Identificador de la lección asociada, si aplica.</summary>
    public int? LeccionId { get; set; }

    /// <summary>Fecha y hora de inicio de la sesión.</summary>
    public DateTime FechaInicio { get; set; }

    /// <summary>Mensaje inicial de bienvenida de la sesión.</summary>
    public string MensajeInicial { get; set; } = string.Empty;
}
