namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Mensaje individual dentro de una sesión de chat con IA.
/// </summary>
public class MensajeChatIA
{
    public int Id { get; set; }
    public Guid SesionId { get; set; }
    public string Rol { get; set; } = string.Empty; // "user", "assistant", "system"
    public string Contenido { get; set; } = string.Empty;
    public bool FueMarcado { get; set; }
    public string? MotivoMarca { get; set; }
    public int TokensUsados { get; set; }
    public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

    // Navegación
    public SesionChatIA Sesion { get; set; } = null!;
}
