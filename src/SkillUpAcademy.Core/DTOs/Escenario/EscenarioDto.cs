namespace SkillUpAcademy.Core.DTOs.Escenario;

/// <summary>
/// Escenario interactivo con opciones de decisión.
/// </summary>
public class EscenarioDto
{
    /// <summary>Identificador del escenario.</summary>
    public int Id { get; set; }

    /// <summary>Texto que describe la situación del escenario.</summary>
    public string TextoSituacion { get; set; } = string.Empty;

    /// <summary>Contexto adicional del escenario.</summary>
    public string? Contexto { get; set; }

    /// <summary>Guion de audio para la narración del escenario.</summary>
    public string? GuionAudio { get; set; }

    /// <summary>Lista de opciones disponibles para el usuario.</summary>
    public IReadOnlyList<OpcionEscenarioDto> Opciones { get; set; } = new List<OpcionEscenarioDto>();
}

/// <summary>
/// Opción de decisión dentro de un escenario.
/// </summary>
public class OpcionEscenarioDto
{
    /// <summary>Identificador de la opción.</summary>
    public int Id { get; set; }

    /// <summary>Texto de la opción.</summary>
    public string TextoOpcion { get; set; } = string.Empty;

    /// <summary>Orden de la opción dentro del escenario.</summary>
    public int Orden { get; set; }
}
