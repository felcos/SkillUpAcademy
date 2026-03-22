namespace SkillUpAcademy.Core.DTOs.Autenticacion;

/// <summary>
/// Petición para actualizar parcialmente el perfil del usuario.
/// </summary>
public class PeticionActualizarPerfil
{
    /// <summary>Nuevo nombre del usuario.</summary>
    public string? Nombre { get; set; }

    /// <summary>Nuevos apellidos del usuario.</summary>
    public string? Apellidos { get; set; }

    /// <summary>Nueva URL del avatar.</summary>
    public string? UrlAvatar { get; set; }

    /// <summary>Indica si se habilita o deshabilita el audio.</summary>
    public bool? AudioHabilitado { get; set; }

    /// <summary>Nuevo código de idioma preferido.</summary>
    public string? IdiomaPreferido { get; set; }
}
