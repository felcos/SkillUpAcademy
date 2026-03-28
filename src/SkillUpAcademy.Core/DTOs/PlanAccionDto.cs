namespace SkillUpAcademy.Core.DTOs;

/// <summary>
/// DTO para mostrar un plan de acción del usuario.
/// </summary>
public class PlanAccionDto
{
    public int Id { get; set; }
    public int LeccionId { get; set; }
    public string TituloLeccion { get; set; } = string.Empty;
    public string Compromiso { get; set; } = string.Empty;
    public string ContextoAplicacion { get; set; } = string.Empty;
    public DateTime FechaObjetivo { get; set; }
    public bool Completado { get; set; }
    public string? ReflexionResultado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaCompletado { get; set; }
}

/// <summary>
/// DTO para crear un nuevo plan de acción.
/// </summary>
public class PeticionCrearPlanAccion
{
    public int LeccionId { get; set; }
    public string Compromiso { get; set; } = string.Empty;
    public string ContextoAplicacion { get; set; } = string.Empty;
    public DateTime FechaObjetivo { get; set; }
}

/// <summary>
/// DTO para completar un plan de acción con reflexión.
/// </summary>
public class PeticionCompletarPlanAccion
{
    public string ReflexionResultado { get; set; } = string.Empty;
}
