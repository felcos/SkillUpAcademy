namespace SkillUpAcademy.Core.DTOs.Autenticacion;

/// <summary>
/// Respuesta devuelta tras un inicio de sesión exitoso.
/// </summary>
public class RespuestaLogin
{
    /// <summary>Token JWT de acceso.</summary>
    public string TokenAcceso { get; set; } = string.Empty;

    /// <summary>Token de renovación para obtener un nuevo token de acceso.</summary>
    public string TokenRenovacion { get; set; } = string.Empty;

    /// <summary>Tiempo en segundos hasta que el token de acceso expire.</summary>
    public int ExpiraEnSegundos { get; set; }

    /// <summary>Perfil del usuario autenticado.</summary>
    public PerfilUsuarioDto Usuario { get; set; } = null!;
}
