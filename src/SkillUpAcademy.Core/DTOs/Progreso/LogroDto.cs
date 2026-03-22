namespace SkillUpAcademy.Core.DTOs.Progreso;

/// <summary>
/// Logro del sistema de gamificación.
/// </summary>
public class LogroDto
{
    /// <summary>Identificador del logro.</summary>
    public int Id { get; set; }

    /// <summary>Slug único del logro.</summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>Título del logro.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Descripción del logro.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Icono del logro.</summary>
    public string? Icono { get; set; }

    /// <summary>Indica si el usuario ha desbloqueado este logro.</summary>
    public bool Desbloqueado { get; set; }

    /// <summary>Fecha y hora en que se desbloqueó, si aplica.</summary>
    public DateTime? FechaDesbloqueo { get; set; }
}
