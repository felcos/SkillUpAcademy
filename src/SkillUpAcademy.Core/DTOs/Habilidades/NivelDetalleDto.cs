namespace SkillUpAcademy.Core.DTOs.Habilidades;

/// <summary>
/// Detalle completo de un nivel, incluyendo sus lecciones.
/// </summary>
public class NivelDetalleDto
{
    /// <summary>Identificador del nivel.</summary>
    public int Id { get; set; }

    /// <summary>Número ordinal del nivel.</summary>
    public int NumeroNivel { get; set; }

    /// <summary>Nombre del nivel.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Descripción del nivel.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Indica si el usuario ha desbloqueado este nivel.</summary>
    public bool Desbloqueado { get; set; }

    /// <summary>Lista de lecciones del nivel.</summary>
    public IReadOnlyList<LeccionResumenDto> Lecciones { get; set; } = new List<LeccionResumenDto>();
}
