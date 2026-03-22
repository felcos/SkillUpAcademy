namespace SkillUpAcademy.Core.DTOs.IA;

/// <summary>
/// Respuesta de la IA a un mensaje del usuario.
/// </summary>
public class RespuestaMensajeIADto
{
    /// <summary>Texto de la respuesta generada por la IA.</summary>
    public string Respuesta { get; set; } = string.Empty;

    /// <summary>URL del audio generado para la respuesta, si aplica.</summary>
    public string? UrlAudio { get; set; }

    /// <summary>Indica si el mensaje fue marcado por moderación.</summary>
    public bool FueMarcado { get; set; }

    /// <summary>Cantidad de tokens consumidos en esta interacción.</summary>
    public int TokensUsados { get; set; }

    /// <summary>Lista de sugerencias de seguimiento para el usuario.</summary>
    public IReadOnlyList<string> Sugerencias { get; set; } = new List<string>();
}
