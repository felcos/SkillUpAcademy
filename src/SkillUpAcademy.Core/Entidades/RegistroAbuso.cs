using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Registro de intentos de abuso de la IA.
/// </summary>
public class RegistroAbuso
{
    public int Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid? SesionId { get; set; }
    public TipoViolacion TipoViolacion { get; set; }
    public string? MensajeOriginal { get; set; }
    public string? MetodoDeteccion { get; set; }
    public AccionTomada AccionTomada { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public SesionChatIA? Sesion { get; set; }
}
