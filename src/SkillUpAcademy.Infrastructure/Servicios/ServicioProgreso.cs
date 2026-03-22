using Microsoft.AspNetCore.Identity;
using SkillUpAcademy.Core.DTOs.Progreso;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de progreso, dashboard y logros del usuario.
/// </summary>
public class ServicioProgreso(
    IRepositorioProgreso _repositorioProgreso,
    IRepositorioAreasHabilidad _repositorioAreas,
    IRepositorioLecciones _repositorioLecciones,
    IRepositorioLogros _repositorioLogros,
    UserManager<UsuarioApp> _userManager) : IServicioProgreso
{
    /// <inheritdoc />
    public async Task<DashboardDto> ObtenerDashboardAsync(Guid usuarioId)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        IReadOnlyList<AreaHabilidad> areas = await _repositorioAreas.ObtenerTodasAsync();
        IReadOnlyList<ProgresoUsuario> todosProgresos = await _repositorioProgreso.ObtenerPorUsuarioAsync(usuarioId);
        int totalLecciones = await _repositorioLecciones.ContarLeccionesActivasAsync();
        int completadas = todosProgresos.Count(p => p.Estado == EstadoProgreso.Completado);

        List<ResumenAreaDto> resumenAreas = new List<ResumenAreaDto>();
        foreach (AreaHabilidad area in areas)
        {
            IReadOnlyList<ProgresoUsuario> progresosArea = await _repositorioProgreso.ObtenerPorUsuarioYAreaAsync(usuarioId, area.Id);
            int completadasEnArea = progresosArea.Count(p => p.Estado == EstadoProgreso.Completado);
            AreaHabilidad? areaCompleta = await _repositorioAreas.ObtenerConNivelesYLeccionesAsync(area.Slug);
            int totalEnArea = areaCompleta?.Niveles.SelectMany(n => n.Lecciones).Count() ?? 0;

            resumenAreas.Add(new ResumenAreaDto
            {
                Slug = area.Slug,
                Titulo = area.Titulo,
                Icono = area.Icono,
                ColorPrimario = area.ColorPrimario,
                Porcentaje = totalEnArea > 0 ? (completadasEnArea * 100) / totalEnArea : 0,
                NivelActual = 1
            });
        }

        // Logros recientes
        IReadOnlyList<LogroUsuario> logrosUsuario = await _repositorioLogros.ObtenerLogrosDeUsuarioAsync(usuarioId);
        List<LogroRecienteDto> logrosRecientes = logrosUsuario
            .OrderByDescending(lu => lu.FechaDesbloqueo)
            .Take(5)
            .Select(lu => new LogroRecienteDto
            {
                Titulo = lu.Logro.Titulo,
                Icono = lu.Logro.Icono,
                FechaDesbloqueo = lu.FechaDesbloqueo
            }).ToList();

        return new DashboardDto
        {
            PuntosTotales = usuario.PuntosTotales,
            LeccionesCompletadas = completadas,
            LeccionesTotales = totalLecciones,
            RachaDias = usuario.RachaDias,
            ResumenAreas = resumenAreas,
            LogrosRecientes = logrosRecientes,
            SiguienteLeccionRecomendada = null // TODO: algoritmo de recomendación
        };
    }

    /// <inheritdoc />
    public async Task<ProgresoAreaDto> ObtenerProgresoAreaAsync(string slugArea, Guid usuarioId)
    {
        AreaHabilidad? area = await _repositorioAreas.ObtenerConNivelesYLeccionesAsync(slugArea);
        if (area == null)
            throw new ExcepcionNoEncontrado("AreaHabilidad", slugArea);

        IReadOnlyList<ProgresoUsuario> progresos = await _repositorioProgreso.ObtenerPorUsuarioYAreaAsync(usuarioId, area.Id);

        List<ProgresoNivelDto> nivelesDto = new List<ProgresoNivelDto>();
        int puntosEnArea = 0;
        int totalCompletadas = 0;
        int totalLecciones = 0;

        foreach (Nivel nivel in area.Niveles.OrderBy(n => n.NumeroNivel))
        {
            int leccionesEnNivel = nivel.Lecciones.Count;
            int completadasEnNivel = progresos.Count(p =>
                p.Estado == EstadoProgreso.Completado &&
                nivel.Lecciones.Any(l => l.Id == p.LeccionId));

            puntosEnArea += progresos
                .Where(p => nivel.Lecciones.Any(l => l.Id == p.LeccionId))
                .Sum(p => p.Puntuacion);

            totalCompletadas += completadasEnNivel;
            totalLecciones += leccionesEnNivel;

            nivelesDto.Add(new ProgresoNivelDto
            {
                NumeroNivel = nivel.NumeroNivel,
                Nombre = nivel.Nombre,
                Desbloqueado = nivel.NumeroNivel == 1 || completadasEnNivel > 0,
                LeccionesCompletadas = completadasEnNivel,
                LeccionesTotales = leccionesEnNivel
            });
        }

        return new ProgresoAreaDto
        {
            Slug = area.Slug,
            Titulo = area.Titulo,
            Porcentaje = totalLecciones > 0 ? (totalCompletadas * 100) / totalLecciones : 0,
            PuntosEnArea = puntosEnArea,
            Niveles = nivelesDto
        };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<LogroDto>> ObtenerLogrosAsync(Guid usuarioId)
    {
        IReadOnlyList<Logro> todosLogros = await _repositorioLogros.ObtenerTodosAsync();
        IReadOnlyList<LogroUsuario> logrosUsuario = await _repositorioLogros.ObtenerLogrosDeUsuarioAsync(usuarioId);

        List<LogroDto> resultado = new List<LogroDto>();
        foreach (Logro logro in todosLogros)
        {
            LogroUsuario? desbloqueado = logrosUsuario.FirstOrDefault(lu => lu.LogroId == logro.Id);
            resultado.Add(new LogroDto
            {
                Id = logro.Id,
                Slug = logro.Slug,
                Titulo = logro.Titulo,
                Descripcion = logro.Descripcion,
                Icono = logro.Icono,
                Desbloqueado = desbloqueado != null,
                FechaDesbloqueo = desbloqueado?.FechaDesbloqueo
            });
        }

        return resultado;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<LogroDto>> VerificarLogrosAsync(Guid usuarioId)
    {
        int completadas = await _repositorioProgreso.ContarCompletadasPorUsuarioAsync(usuarioId);
        IReadOnlyList<Logro> todosLogros = await _repositorioLogros.ObtenerTodosAsync();
        List<LogroDto> nuevosLogros = new List<LogroDto>();

        // Verificar logro "first_lesson"
        if (completadas >= 1 && !await _repositorioLogros.TieneLogroAsync(usuarioId, "first_lesson"))
        {
            Logro? logro = todosLogros.FirstOrDefault(l => l.Slug == "first_lesson");
            if (logro != null)
            {
                await _repositorioLogros.DesbloquearAsync(usuarioId, logro.Id);
                nuevosLogros.Add(new LogroDto
                {
                    Id = logro.Id,
                    Slug = logro.Slug,
                    Titulo = logro.Titulo,
                    Icono = logro.Icono,
                    Desbloqueado = true,
                    FechaDesbloqueo = DateTime.UtcNow
                });
            }
        }

        // Verificar logro "points_100"
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario != null && usuario.PuntosTotales >= 100 && !await _repositorioLogros.TieneLogroAsync(usuarioId, "points_100"))
        {
            Logro? logro = todosLogros.FirstOrDefault(l => l.Slug == "points_100");
            if (logro != null)
            {
                await _repositorioLogros.DesbloquearAsync(usuarioId, logro.Id);
                nuevosLogros.Add(new LogroDto
                {
                    Id = logro.Id,
                    Slug = logro.Slug,
                    Titulo = logro.Titulo,
                    Icono = logro.Icono,
                    Desbloqueado = true,
                    FechaDesbloqueo = DateTime.UtcNow
                });
            }
        }

        return nuevosLogros;
    }

    /// <inheritdoc />
    public async Task ActualizarRachaAsync(Guid usuarioId)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null) return;

        DateTime hoy = DateTime.UtcNow.Date;

        if (usuario.UltimaFechaActividad == null || usuario.UltimaFechaActividad.Value.Date < hoy.AddDays(-1))
        {
            // Racha rota o primera vez
            usuario.RachaDias = 1;
        }
        else if (usuario.UltimaFechaActividad.Value.Date == hoy.AddDays(-1))
        {
            // Día consecutivo
            usuario.RachaDias++;
        }
        // Si es el mismo día, no hacer nada

        usuario.UltimaFechaActividad = hoy;
        await _userManager.UpdateAsync(usuario);
    }
}
