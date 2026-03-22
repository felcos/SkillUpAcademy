using SkillUpAcademy.Core.DTOs.Habilidades;

namespace SkillUpAcademy.Core.DTOs.Progreso;

/// <summary>
/// Datos del dashboard principal del usuario.
/// </summary>
public class DashboardDto
{
    /// <summary>Puntos totales acumulados por el usuario.</summary>
    public int PuntosTotales { get; set; }

    /// <summary>Cantidad de lecciones completadas en total.</summary>
    public int LeccionesCompletadas { get; set; }

    /// <summary>Cantidad total de lecciones disponibles.</summary>
    public int LeccionesTotales { get; set; }

    /// <summary>Cantidad de días consecutivos de actividad.</summary>
    public int RachaDias { get; set; }

    /// <summary>Resumen del progreso por cada área de habilidad.</summary>
    public IReadOnlyList<ResumenAreaDto> ResumenAreas { get; set; } = new List<ResumenAreaDto>();

    /// <summary>Logros desbloqueados recientemente.</summary>
    public IReadOnlyList<LogroRecienteDto> LogrosRecientes { get; set; } = new List<LogroRecienteDto>();

    /// <summary>Siguiente lección recomendada para el usuario.</summary>
    public LeccionRecomendadaDto? SiguienteLeccionRecomendada { get; set; }
}

/// <summary>
/// Resumen compacto del progreso en un área para el dashboard.
/// </summary>
public class ResumenAreaDto
{
    /// <summary>Slug único del área.</summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>Título del área.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Icono del área.</summary>
    public string? Icono { get; set; }

    /// <summary>Color primario del área.</summary>
    public string? ColorPrimario { get; set; }

    /// <summary>Porcentaje de avance (0-100).</summary>
    public int Porcentaje { get; set; }

    /// <summary>Nivel actual del usuario en esta área.</summary>
    public int NivelActual { get; set; }
}

/// <summary>
/// Logro desbloqueado recientemente.
/// </summary>
public class LogroRecienteDto
{
    /// <summary>Título del logro.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Icono del logro.</summary>
    public string? Icono { get; set; }

    /// <summary>Fecha y hora en que se desbloqueó el logro.</summary>
    public DateTime FechaDesbloqueo { get; set; }
}

/// <summary>
/// Lección recomendada como siguiente paso para el usuario.
/// </summary>
public class LeccionRecomendadaDto
{
    /// <summary>Identificador de la lección.</summary>
    public int Id { get; set; }

    /// <summary>Título de la lección.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Nombre del área de habilidad a la que pertenece.</summary>
    public string AreaHabilidad { get; set; } = string.Empty;

    /// <summary>Tipo de lección.</summary>
    public string TipoLeccion { get; set; } = string.Empty;
}
