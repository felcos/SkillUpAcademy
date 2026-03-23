namespace SkillUpAcademy.Core.DTOs.Admin;

/// <summary>
/// Datos de un usuario vistos desde el panel de administración.
/// </summary>
public record UsuarioAdmin(
    Guid Id,
    string Email,
    string NombreCompleto,
    DateTime FechaRegistro,
    DateTime? UltimoAcceso,
    int LeccionesCompletadas,
    int LogrosDesbloqueados,
    double ProgresoGeneral,
    bool EstaBloqueadoIA
);
