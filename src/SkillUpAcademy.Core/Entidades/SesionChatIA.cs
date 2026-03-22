using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Sesión de chat con la IA (Aria).
/// </summary>
public class SesionChatIA
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UsuarioId { get; set; }
    public int? LeccionId { get; set; }
    public TipoSesionIA TipoSesion { get; set; }
    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public DateTime? FechaFin { get; set; }
    public int ContadorMensajes { get; set; }
    public bool FueMarcada { get; set; }
    public bool Activa { get; set; } = true;

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public Leccion? Leccion { get; set; }
    public ICollection<MensajeChatIA> Mensajes { get; set; } = new List<MensajeChatIA>();
}
