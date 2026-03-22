namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Nivel dentro de un área de habilidad (Fundamentos, Práctica, Dominio).
/// </summary>
public class Nivel
{
    public int Id { get; set; }

    /// <summary>FK al área de habilidad.</summary>
    public int AreaHabilidadId { get; set; }

    /// <summary>Número del nivel (1, 2, 3).</summary>
    public int NumeroNivel { get; set; }

    /// <summary>Nombre del nivel.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Descripción del nivel.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Puntos mínimos para desbloquear este nivel.</summary>
    public int PuntosDesbloqueo { get; set; }

    /// <summary>Indica si el nivel está activo.</summary>
    public bool Activo { get; set; } = true;

    // Navegación
    public AreaHabilidad AreaHabilidad { get; set; } = null!;
    public ICollection<Leccion> Lecciones { get; set; } = new List<Leccion>();
}
