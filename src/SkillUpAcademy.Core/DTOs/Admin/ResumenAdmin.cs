namespace SkillUpAcademy.Core.DTOs.Admin;

/// <summary>
/// Resumen general del panel de administración.
/// </summary>
public record ResumenAdmin(
    int TotalUsuarios,
    int UsuariosActivos7Dias,
    int TotalSesionesIA,
    int TotalMensajesIA,
    int TotalLeccionesCompletadas,
    int TotalQuizzesCompletados,
    double PromedioProgreso,
    IReadOnlyList<ActividadDiaria> ActividadUltimos30Dias
);

/// <summary>
/// Actividad agregada de un día específico.
/// </summary>
public record ActividadDiaria(
    DateOnly Fecha,
    int UsuariosActivos,
    int LeccionesCompletadas,
    int MensajesIA
);
