using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.Autenticacion;

/// <summary>
/// Petición para renovar un token de acceso expirado.
/// </summary>
/// <param name="TokenRenovacion">Token de renovación vigente.</param>
public record PeticionRenovarToken([Required] string TokenRenovacion);
