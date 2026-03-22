using SkillUpAcademy.Core.DTOs.Habilidades;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de áreas de habilidad y niveles.
/// </summary>
public class ServicioHabilidades(
    IRepositorioAreasHabilidad _repositorioAreas,
    IRepositorioProgreso _repositorioProgreso) : IServicioHabilidades
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<AreaHabilidadDto>> ObtenerAreasAsync(Guid? usuarioId = null)
    {
        IReadOnlyList<AreaHabilidad> areas = await _repositorioAreas.ObtenerTodasAsync();

        List<AreaHabilidadDto> resultado = new List<AreaHabilidadDto>();
        foreach (AreaHabilidad area in areas)
        {
            AreaHabilidadDto dto = new AreaHabilidadDto
            {
                Id = area.Id,
                Slug = area.Slug,
                Titulo = area.Titulo,
                Subtitulo = area.Subtitulo,
                Icono = area.Icono,
                ColorPrimario = area.ColorPrimario
            };

            if (usuarioId.HasValue)
            {
                dto.Progreso = await CalcularProgresoArea(area.Id, usuarioId.Value);
            }

            resultado.Add(dto);
        }

        return resultado;
    }

    /// <inheritdoc />
    public async Task<AreaHabilidadDetalleDto> ObtenerAreaPorSlugAsync(string slug, Guid? usuarioId = null)
    {
        AreaHabilidad? area = await _repositorioAreas.ObtenerConNivelesYLeccionesAsync(slug);
        if (area == null)
            throw new ExcepcionNoEncontrado("AreaHabilidad", slug);

        List<NivelDto> nivelesDto = new List<NivelDto>();
        foreach (Nivel nivel in area.Niveles)
        {
            int leccionesCompletadas = 0;
            if (usuarioId.HasValue)
            {
                leccionesCompletadas = await _repositorioProgreso.ContarCompletadasPorUsuarioYNivelAsync(usuarioId.Value, nivel.Id);
            }

            nivelesDto.Add(new NivelDto
            {
                Id = nivel.Id,
                NumeroNivel = nivel.NumeroNivel,
                Nombre = nivel.Nombre,
                Descripcion = nivel.Descripcion,
                PuntosDesbloqueo = nivel.PuntosDesbloqueo,
                Desbloqueado = true, // Nivel 1 siempre desbloqueado, otros por lógica futura
                LeccionesCompletadas = leccionesCompletadas,
                LeccionesTotales = nivel.Lecciones.Count
            });
        }

        AreaHabilidadDetalleDto resultado = new AreaHabilidadDetalleDto
        {
            Id = area.Id,
            Slug = area.Slug,
            Titulo = area.Titulo,
            Subtitulo = area.Subtitulo,
            Descripcion = area.Descripcion,
            Icono = area.Icono,
            ColorPrimario = area.ColorPrimario,
            ColorAcento = area.ColorAcento,
            Niveles = nivelesDto
        };

        if (usuarioId.HasValue)
        {
            resultado.Progreso = await CalcularProgresoArea(area.Id, usuarioId.Value);
        }

        return resultado;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<NivelDto>> ObtenerNivelesAsync(string slugArea, Guid usuarioId)
    {
        AreaHabilidadDetalleDto detalle = await ObtenerAreaPorSlugAsync(slugArea, usuarioId);
        return detalle.Niveles;
    }

    /// <inheritdoc />
    public async Task<NivelDetalleDto> ObtenerNivelAsync(string slugArea, int numeroNivel, Guid usuarioId)
    {
        AreaHabilidad? area = await _repositorioAreas.ObtenerConNivelesYLeccionesAsync(slugArea);
        if (area == null)
            throw new ExcepcionNoEncontrado("AreaHabilidad", slugArea);

        Nivel? nivel = area.Niveles.FirstOrDefault(n => n.NumeroNivel == numeroNivel);
        if (nivel == null)
            throw new ExcepcionNoEncontrado("Nivel", numeroNivel);

        IReadOnlyList<ProgresoUsuario> progresos = await _repositorioProgreso.ObtenerPorUsuarioYAreaAsync(usuarioId, area.Id);

        List<LeccionResumenDto> leccionesDto = new List<LeccionResumenDto>();
        foreach (Leccion leccion in nivel.Lecciones)
        {
            ProgresoUsuario? progreso = progresos.FirstOrDefault(p => p.LeccionId == leccion.Id);
            leccionesDto.Add(new LeccionResumenDto
            {
                Id = leccion.Id,
                Titulo = leccion.Titulo,
                TipoLeccion = leccion.TipoLeccion.ToString(),
                DuracionMinutos = leccion.DuracionMinutos,
                PuntosRecompensa = leccion.PuntosRecompensa,
                Estado = progreso?.Estado.ToString() ?? "NoIniciado",
                Puntuacion = progreso?.Puntuacion
            });
        }

        return new NivelDetalleDto
        {
            Id = nivel.Id,
            NumeroNivel = nivel.NumeroNivel,
            Nombre = nivel.Nombre,
            Descripcion = nivel.Descripcion,
            Desbloqueado = true,
            Lecciones = leccionesDto
        };
    }

    private async Task<ProgresoResumenDto> CalcularProgresoArea(int areaId, Guid usuarioId)
    {
        IReadOnlyList<ProgresoUsuario> progresos = await _repositorioProgreso.ObtenerPorUsuarioYAreaAsync(usuarioId, areaId);
        int completadas = progresos.Count(p => p.Estado == EstadoProgreso.Completado);
        // Get total lessons from the progresos context or a separate call
        int total = progresos.Count > 0 ? progresos.Count : 0;
        // We need the actual total, use a different approach
        AreaHabilidad? area = await _repositorioAreas.ObtenerConNivelesYLeccionesAsync(
            (await _repositorioAreas.ObtenerPorIdAsync(areaId))?.Slug ?? "");

        int totalLecciones = area?.Niveles.SelectMany(n => n.Lecciones).Count() ?? 0;

        return new ProgresoResumenDto
        {
            LeccionesCompletadas = completadas,
            LeccionesTotales = totalLecciones,
            Porcentaje = totalLecciones > 0 ? (completadas * 100) / totalLecciones : 0,
            NivelActual = DeterminarNivelActual(progresos, area)
        };
    }

    private static int DeterminarNivelActual(IReadOnlyList<ProgresoUsuario> progresos, AreaHabilidad? area)
    {
        if (area == null || progresos.Count == 0)
            return 1;

        int nivelMaximo = 1;
        foreach (Nivel nivel in area.Niveles.OrderBy(n => n.NumeroNivel))
        {
            int leccionesEnNivel = nivel.Lecciones.Count;
            int completadasEnNivel = progresos.Count(p =>
                p.Estado == EstadoProgreso.Completado &&
                nivel.Lecciones.Any(l => l.Id == p.LeccionId));

            if (completadasEnNivel >= leccionesEnNivel && leccionesEnNivel > 0)
            {
                nivelMaximo = nivel.NumeroNivel + 1;
            }
            else
            {
                nivelMaximo = nivel.NumeroNivel;
                break;
            }
        }

        return Math.Min(nivelMaximo, 3);
    }
}
