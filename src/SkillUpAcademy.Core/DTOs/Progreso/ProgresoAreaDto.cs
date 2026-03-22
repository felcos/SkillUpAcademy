namespace SkillUpAcademy.Core.DTOs.Progreso;

/// <summary>
/// Progreso detallado del usuario en un área de habilidad.
/// </summary>
public class ProgresoAreaDto
{
    /// <summary>Slug único del área.</summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>Título del área.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Porcentaje de avance (0-100).</summary>
    public int Porcentaje { get; set; }

    /// <summary>Puntos acumulados en esta área.</summary>
    public int PuntosEnArea { get; set; }

    /// <summary>Progreso desglosado por nivel.</summary>
    public IReadOnlyList<ProgresoNivelDto> Niveles { get; set; } = new List<ProgresoNivelDto>();
}

/// <summary>
/// Progreso del usuario en un nivel específico.
/// </summary>
public class ProgresoNivelDto
{
    /// <summary>Número ordinal del nivel.</summary>
    public int NumeroNivel { get; set; }

    /// <summary>Nombre del nivel.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Indica si el usuario ha desbloqueado este nivel.</summary>
    public bool Desbloqueado { get; set; }

    /// <summary>Cantidad de lecciones completadas en este nivel.</summary>
    public int LeccionesCompletadas { get; set; }

    /// <summary>Cantidad total de lecciones en este nivel.</summary>
    public int LeccionesTotales { get; set; }
}
