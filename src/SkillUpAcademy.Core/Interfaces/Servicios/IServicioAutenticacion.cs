using SkillUpAcademy.Core.DTOs.Autenticacion;

namespace SkillUpAcademy.Core.Interfaces.Servicios;

/// <summary>
/// Servicio de autenticación y gestión de usuarios.
/// </summary>
public interface IServicioAutenticacion
{
    /// <summary>Registra un nuevo usuario.</summary>
    Task<RespuestaLogin> RegistrarAsync(PeticionRegistro peticion);

    /// <summary>Inicia sesión y devuelve tokens.</summary>
    Task<RespuestaLogin> IniciarSesionAsync(PeticionLogin peticion);

    /// <summary>Renueva el token de acceso con un refresh token.</summary>
    Task<RespuestaLogin> RenovarTokenAsync(PeticionRenovarToken peticion);

    /// <summary>Cierra sesión invalidando el refresh token.</summary>
    Task CerrarSesionAsync(Guid usuarioId);

    /// <summary>Obtiene el perfil del usuario.</summary>
    Task<PerfilUsuarioDto> ObtenerPerfilAsync(Guid usuarioId);

    /// <summary>Actualiza el perfil del usuario.</summary>
    Task<PerfilUsuarioDto> ActualizarPerfilAsync(Guid usuarioId, PeticionActualizarPerfil peticion);

    /// <summary>Cambia la contraseña del usuario.</summary>
    Task CambiarContrasenaAsync(Guid usuarioId, string contrasenaActual, string contrasenaNueva);
}
