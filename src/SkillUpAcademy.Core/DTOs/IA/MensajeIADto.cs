namespace SkillUpAcademy.Core.DTOs.IA;

/// <summary>
/// Mensaje individual dentro de una sesión de IA.
/// </summary>
public class MensajeIADto
{
    /// <summary>Identificador del mensaje.</summary>
    public int Id { get; set; }

    /// <summary>Rol del emisor (usuario o asistente).</summary>
    public string Rol { get; set; } = string.Empty;

    /// <summary>Contenido textual del mensaje.</summary>
    public string Contenido { get; set; } = string.Empty;

    /// <summary>Fecha y hora de envío del mensaje.</summary>
    public DateTime FechaEnvio { get; set; }
}
