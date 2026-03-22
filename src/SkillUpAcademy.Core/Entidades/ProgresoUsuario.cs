using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Progreso de un usuario en una lección específica.
/// </summary>
public class ProgresoUsuario
{
    public int Id { get; set; }
    public Guid UsuarioId { get; set; }
    public int LeccionId { get; set; }
    public EstadoProgreso Estado { get; set; } = EstadoProgreso.NoIniciado;
    public int Puntuacion { get; set; }
    public int Intentos { get; set; }
    public DateTime? FechaCompletado { get; set; }
    public DateTime UltimoAcceso { get; set; } = DateTime.UtcNow;

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public Leccion Leccion { get; set; } = null!;
}
