namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Escenario interactivo dentro de una lección.
/// </summary>
public class Escenario
{
    public int Id { get; set; }
    public int LeccionId { get; set; }
    public string TextoSituacion { get; set; } = string.Empty;
    public string? Contexto { get; set; }
    public string? GuionAudio { get; set; }

    // Navegación
    public Leccion Leccion { get; set; } = null!;
    public ICollection<OpcionEscenario> Opciones { get; set; } = new List<OpcionEscenario>();
}
