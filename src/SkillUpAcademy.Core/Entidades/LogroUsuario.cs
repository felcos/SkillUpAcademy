namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Relación entre un usuario y un logro desbloqueado.
/// </summary>
public class LogroUsuario
{
    public int Id { get; set; }
    public Guid UsuarioId { get; set; }
    public int LogroId { get; set; }
    public DateTime FechaDesbloqueo { get; set; } = DateTime.UtcNow;

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public Logro Logro { get; set; } = null!;
}
