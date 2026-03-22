using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Opción de elección en un escenario interactivo.
/// </summary>
public class OpcionEscenario
{
    public int Id { get; set; }
    public int EscenarioId { get; set; }
    public string TextoOpcion { get; set; } = string.Empty;
    public TipoResultadoEscenario TipoResultado { get; set; }
    public string TextoRetroalimentacion { get; set; } = string.Empty;
    public int PuntosOtorgados { get; set; }
    public int? SiguienteEscenarioId { get; set; }
    public int Orden { get; set; }

    // Navegación
    public Escenario Escenario { get; set; } = null!;
    public Escenario? SiguienteEscenario { get; set; }
}
