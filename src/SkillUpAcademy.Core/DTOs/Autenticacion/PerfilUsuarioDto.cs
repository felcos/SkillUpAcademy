namespace SkillUpAcademy.Core.DTOs.Autenticacion;

/// <summary>
/// Datos del perfil de un usuario.
/// </summary>
public class PerfilUsuarioDto
{
    /// <summary>Identificador único del usuario.</summary>
    public Guid Id { get; set; }

    /// <summary>Correo electrónico del usuario.</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Nombre del usuario.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Apellidos del usuario.</summary>
    public string Apellidos { get; set; } = string.Empty;

    /// <summary>URL del avatar del usuario.</summary>
    public string? UrlAvatar { get; set; }

    /// <summary>Puntos totales acumulados por el usuario.</summary>
    public int PuntosTotales { get; set; }

    /// <summary>Cantidad de días consecutivos de actividad.</summary>
    public int RachaDias { get; set; }

    /// <summary>Indica si el usuario tiene habilitado el audio.</summary>
    public bool AudioHabilitado { get; set; }

    /// <summary>Código del idioma preferido por el usuario.</summary>
    public string IdiomaPreferido { get; set; } = "es";
}
