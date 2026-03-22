using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.Autenticacion;

/// <summary>
/// Petición para registrar un nuevo usuario en la plataforma.
/// </summary>
/// <param name="Email">Correo electrónico del usuario.</param>
/// <param name="Contrasena">Contraseña del usuario (mínimo 8 caracteres).</param>
/// <param name="Nombre">Nombre del usuario.</param>
/// <param name="Apellidos">Apellidos del usuario.</param>
public record PeticionRegistro(
    [Required][EmailAddress] string Email,
    [Required][MinLength(8)] string Contrasena,
    [Required][MaxLength(100)] string Nombre,
    [Required][MaxLength(100)] string Apellidos
);
