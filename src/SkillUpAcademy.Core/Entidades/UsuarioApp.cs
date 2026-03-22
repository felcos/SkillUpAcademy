using Microsoft.AspNetCore.Identity;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Usuario de la aplicación, extiende IdentityUser con campos adicionales.
/// </summary>
public class UsuarioApp : IdentityUser<Guid>
{
    /// <summary>Nombre del usuario.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Apellidos del usuario.</summary>
    public string Apellidos { get; set; } = string.Empty;

    /// <summary>URL del avatar del usuario.</summary>
    public string? UrlAvatar { get; set; }

    /// <summary>Fecha de creación de la cuenta.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    /// <summary>Fecha del último inicio de sesión.</summary>
    public DateTime UltimoAcceso { get; set; } = DateTime.UtcNow;

    /// <summary>Puntos totales acumulados.</summary>
    public int PuntosTotales { get; set; }

    /// <summary>Idioma preferido del usuario.</summary>
    public string IdiomaPreferido { get; set; } = "es";

    /// <summary>Indica si la cuenta está activa.</summary>
    public bool Activo { get; set; } = true;

    /// <summary>Indica si el audio está habilitado.</summary>
    public bool AudioHabilitado { get; set; } = true;

    /// <summary>Días consecutivos de actividad.</summary>
    public int RachaDias { get; set; }

    /// <summary>Fecha del último día de actividad para calcular racha.</summary>
    public DateTime? UltimaFechaActividad { get; set; }

    // Navegación
    public ICollection<ProgresoUsuario> Progresos { get; set; } = new List<ProgresoUsuario>();
    public ICollection<RespuestaQuizUsuario> RespuestasQuiz { get; set; } = new List<RespuestaQuizUsuario>();
    public ICollection<EleccionEscenarioUsuario> EleccionesEscenario { get; set; } = new List<EleccionEscenarioUsuario>();
    public ICollection<SesionChatIA> SesionesChatIA { get; set; } = new List<SesionChatIA>();
    public ICollection<LogroUsuario> Logros { get; set; } = new List<LogroUsuario>();
}
