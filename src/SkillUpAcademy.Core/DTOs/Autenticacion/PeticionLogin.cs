using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.Autenticacion;

/// <summary>
/// Petición para iniciar sesión en la plataforma.
/// </summary>
/// <param name="Email">Correo electrónico del usuario.</param>
/// <param name="Contrasena">Contraseña del usuario.</param>
public record PeticionLogin(
    [Required][EmailAddress] string Email,
    [Required] string Contrasena
);
