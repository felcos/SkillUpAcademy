namespace SkillUpAcademy.Core.DTOs.Escenario;

/// <summary>
/// Resultado de la elección del usuario en un escenario.
/// </summary>
public class ResultadoEscenarioDto
{
    /// <summary>Tipo de resultado (positivo, negativo, neutral).</summary>
    public string TipoResultado { get; set; } = string.Empty;

    /// <summary>Texto de retroalimentación sobre la decisión.</summary>
    public string TextoRetroalimentacion { get; set; } = string.Empty;

    /// <summary>Puntos otorgados por la decisión.</summary>
    public int PuntosOtorgados { get; set; }

    /// <summary>Siguiente escenario en la cadena, si existe.</summary>
    public EscenarioDto? SiguienteEscenario { get; set; }

    /// <summary>Narración generada por IA sobre el resultado.</summary>
    public string? NarracionIA { get; set; }
}
