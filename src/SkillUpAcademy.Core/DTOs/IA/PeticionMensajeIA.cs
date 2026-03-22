using System.ComponentModel.DataAnnotations;

namespace SkillUpAcademy.Core.DTOs.IA;

/// <summary>
/// Petición para enviar un mensaje a la IA dentro de una sesión activa.
/// </summary>
/// <param name="Mensaje">Texto del mensaje del usuario (máximo 1000 caracteres).</param>
public record PeticionMensajeIA(
    [Required][MaxLength(1000)] string Mensaje
);
