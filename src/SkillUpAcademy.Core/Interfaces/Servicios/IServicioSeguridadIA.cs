namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de seguridad anti-abuso para la IA. 5 capas de protección.
/// </summary>
public interface IServicioSeguridadIA
{
    /// <summary>Valida un mensaje del usuario antes de enviarlo a la IA.</summary>
    Task<ResultadoValidacionIA> ValidarEntradaAsync(string mensaje, Guid usuarioId, Guid sesionId);

    /// <summary>Valida la respuesta de la IA antes de enviarla al usuario.</summary>
    Task<ResultadoValidacionIA> ValidarSalidaAsync(string respuesta);

    /// <summary>Registra un strike de abuso para el usuario.</summary>
    Task RegistrarStrikeAsync(Guid usuarioId, Guid sesionId, string tipoViolacion, string mensajeOriginal, string metodoDeteccion);

    /// <summary>Verifica si el usuario está bloqueado por abuso.</summary>
    Task<bool> UsuarioBloqueadoAsync(Guid usuarioId);
}

/// <summary>
/// Resultado de la validación de seguridad IA.
/// </summary>
public class ResultadoValidacionIA
{
    /// <summary>Si el contenido es seguro.</summary>
    public bool EsSeguro { get; set; }

    /// <summary>Razón del rechazo (si no es seguro).</summary>
    public string? Razon { get; set; }

    /// <summary>Categoría de violación detectada.</summary>
    public string? CategoriaViolacion { get; set; }

    /// <summary>Mensaje seguro alternativo para mostrar al usuario.</summary>
    public string? MensajeAlternativo { get; set; }
}
