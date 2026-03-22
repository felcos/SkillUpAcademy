namespace SkillUpAcademy.Core.DTOs.Habilidades;

/// <summary>
/// Resumen de un nivel dentro de un área de habilidad.
/// </summary>
public class NivelDto
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

    /// <summary>Puntos necesarios para desbloquear el nivel.</summary>
    public int PuntosDesbloqueo { get; set; }

    /// <summary>Cantidad de lecciones completadas en este nivel.</summary>
    public int LeccionesCompletadas { get; set; }

    /// <summary>Cantidad total de lecciones en este nivel.</summary>
    public int LeccionesTotales { get; set; }
}
