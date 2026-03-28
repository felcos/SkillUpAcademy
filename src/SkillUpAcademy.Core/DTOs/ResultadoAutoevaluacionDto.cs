namespace SkillUpAcademy.Core.DTOs;

/// <summary>
/// DTO para mostrar un resultado de autoevaluación.
/// </summary>
public class ResultadoAutoevaluacionDto
{
    public int Id { get; set; }
    public int LeccionId { get; set; }
    public string TituloLeccion { get; set; } = string.Empty;
    public string NivelDetectado { get; set; } = string.Empty;
    public string? ResumenIA { get; set; }
    public DateTime FechaEvaluacion { get; set; }
}

/// <summary>
/// DTO para guardar un resultado de autoevaluación.
/// </summary>
public class PeticionGuardarAutoevaluacion
{
    public int LeccionId { get; set; }
    public string RespuestasJson { get; set; } = string.Empty;
    public string NivelDetectado { get; set; } = string.Empty;
    public string? ResumenIA { get; set; }
}
