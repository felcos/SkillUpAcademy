namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Logro/Achievement desbloqueable.
/// </summary>
public class Logro
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? Icono { get; set; }
    public int PuntosRequeridos { get; set; }
    public string? Condicion { get; set; }

    // Navegación
    public ICollection<LogroUsuario> LogrosUsuario { get; set; } = new List<LogroUsuario>();
}
