namespace SkillUpAcademy.Core.DTOs.Admin;

/// <summary>
/// Estadísticas globales del contenido educativo.
/// </summary>
public record EstadisticasContenido(
    int TotalAreas,
    int TotalNiveles,
    int TotalLecciones,
    int TotalPreguntas,
    int TotalEscenarios,
    int TotalEscenas,
    IReadOnlyList<AreaEstadistica> AreasPorCompletados
);

/// <summary>
/// Estadísticas de completados y calificación de un área.
/// </summary>
public record AreaEstadistica(
    string NombreArea,
    int VecesCompletada,
    double PromedioCalificacion
);
