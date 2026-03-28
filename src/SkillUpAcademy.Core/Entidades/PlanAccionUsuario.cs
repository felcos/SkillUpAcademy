namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Compromiso de acción medible creado por el usuario en una lección de tipo PlanAccion.
/// </summary>
public class PlanAccionUsuario
{
    public int Id { get; set; }

    /// <summary>FK al usuario que creó el plan.</summary>
    public Guid UsuarioId { get; set; }

    /// <summary>FK a la lección de tipo PlanAccion asociada.</summary>
    public int LeccionId { get; set; }

    /// <summary>Compromiso concreto del usuario (ej: "Voy a practicar escucha activa con mi jefe").</summary>
    public string Compromiso { get; set; } = string.Empty;

    /// <summary>Contexto donde aplicará el compromiso (ej: "En la reunión semanal del martes").</summary>
    public string ContextoAplicacion { get; set; } = string.Empty;

    /// <summary>Fecha objetivo para cumplir el compromiso.</summary>
    public DateTime FechaObjetivo { get; set; }

    /// <summary>Indica si el compromiso fue completado.</summary>
    public bool Completado { get; set; }

    /// <summary>Reflexión del usuario sobre qué pasó cuando intentó cumplir el compromiso.</summary>
    public string? ReflexionResultado { get; set; }

    /// <summary>Fecha de creación del plan.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    /// <summary>Fecha en que se marcó como completado.</summary>
    public DateTime? FechaCompletado { get; set; }

    // Navegación
    public UsuarioApp Usuario { get; set; } = null!;
    public Leccion Leccion { get; set; } = null!;
}
