namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Elección de un usuario en un escenario.
/// </summary>
public class EleccionEscenarioUsuario
{
    public int Id { get; set; }
    public Guid UsuarioId { get; set; }
    public int EscenarioId { get; set; }
    public int OpcionEscenarioId { get; set; }
    public DateTime FechaEleccion { get; set; } = DateTime.UtcNow;

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public Escenario Escenario { get; set; } = null!;
    public OpcionEscenario OpcionEscenario { get; set; } = null!;
}
