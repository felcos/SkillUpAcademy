using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.DTOs.Admin;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de administración: resumen, usuarios y estadísticas de contenido.
/// </summary>
public class ServicioAdmin(
    AppDbContext _contexto,
    UserManager<UsuarioApp> _userManager) : IServicioAdmin
{
    /// <inheritdoc />
    public async Task<ResumenAdmin> ObtenerResumenAsync()
    {
        int totalUsuarios = await _userManager.Users.CountAsync();

        DateTime hace7Dias = DateTime.UtcNow.AddDays(-7);
        int usuariosActivos7Dias = await _userManager.Users
            .CountAsync(u => u.UltimoAcceso >= hace7Dias);

        int totalSesionesIA = await _contexto.SesionesChatIA.CountAsync();
        int totalMensajesIA = await _contexto.MensajesChatIA.CountAsync();

        int totalLeccionesCompletadas = await _contexto.ProgresosUsuario
            .CountAsync(p => p.Estado == EstadoProgreso.Completado);

        int totalQuizzesCompletados = await _contexto.RespuestasQuizUsuario
            .Select(r => new { r.UsuarioId, r.PreguntaQuiz.Leccion.Id })
            .Distinct()
            .CountAsync();

        int totalLecciones = await _contexto.Lecciones.CountAsync(l => l.Activo);
        double promedioProgreso = totalLecciones > 0 && totalUsuarios > 0
            ? Math.Round((double)totalLeccionesCompletadas / totalUsuarios / totalLecciones * 100, 1)
            : 0;

        IReadOnlyList<ActividadDiaria> actividadUltimos30Dias = await ObtenerActividadUltimos30DiasAsync();

        return new ResumenAdmin(
            totalUsuarios,
            usuariosActivos7Dias,
            totalSesionesIA,
            totalMensajesIA,
            totalLeccionesCompletadas,
            totalQuizzesCompletados,
            promedioProgreso,
            actividadUltimos30Dias
        );
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<UsuarioAdmin>> ObtenerUsuariosAsync(int pagina, int tamano)
    {
        int totalLecciones = await _contexto.Lecciones.CountAsync(l => l.Activo);

        List<UsuarioApp> usuarios = await _userManager.Users
            .OrderByDescending(u => u.FechaCreacion)
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .ToListAsync();

        List<UsuarioAdmin> resultado = new List<UsuarioAdmin>();

        foreach (UsuarioApp usuario in usuarios)
        {
            int leccionesCompletadas = await _contexto.ProgresosUsuario
                .CountAsync(p => p.UsuarioId == usuario.Id && p.Estado == EstadoProgreso.Completado);

            int logrosDesbloqueados = await _contexto.LogrosUsuario
                .CountAsync(l => l.UsuarioId == usuario.Id);

            double progresoGeneral = totalLecciones > 0
                ? Math.Round((double)leccionesCompletadas / totalLecciones * 100, 1)
                : 0;

            resultado.Add(new UsuarioAdmin(
                usuario.Id,
                usuario.Email ?? string.Empty,
                $"{usuario.Nombre} {usuario.Apellidos}".Trim(),
                usuario.FechaCreacion,
                usuario.UltimoAcceso,
                leccionesCompletadas,
                logrosDesbloqueados,
                progresoGeneral,
                usuario.EstaBloqueadoIA
            ));
        }

        return resultado;
    }

    /// <inheritdoc />
    public async Task<int> ObtenerTotalUsuariosAsync()
    {
        return await _userManager.Users.CountAsync();
    }

    /// <inheritdoc />
    public async Task<EstadisticasContenido> ObtenerEstadisticasContenidoAsync()
    {
        int totalAreas = await _contexto.AreasHabilidad.CountAsync(a => a.Activo);
        int totalNiveles = await _contexto.Niveles.CountAsync(n => n.Activo);
        int totalLecciones = await _contexto.Lecciones.CountAsync(l => l.Activo);
        int totalPreguntas = await _contexto.PreguntasQuiz.CountAsync();
        int totalEscenarios = await _contexto.Escenarios.CountAsync();
        int totalEscenas = await _contexto.EscenasLeccion.CountAsync();

        List<AreaEstadistica> areasPorCompletados = await _contexto.AreasHabilidad
            .Where(a => a.Activo)
            .Select(a => new AreaEstadistica(
                a.Titulo,
                a.Niveles
                    .SelectMany(n => n.Lecciones)
                    .SelectMany(l => l.Progresos)
                    .Count(p => p.Estado == EstadoProgreso.Completado),
                a.Niveles
                    .SelectMany(n => n.Lecciones)
                    .SelectMany(l => l.Progresos)
                    .Where(p => p.Estado == EstadoProgreso.Completado && p.Puntuacion > 0)
                    .Select(p => (double)p.Puntuacion)
                    .DefaultIfEmpty(0)
                    .Average()
            ))
            .OrderByDescending(a => a.VecesCompletada)
            .ToListAsync();

        return new EstadisticasContenido(
            totalAreas,
            totalNiveles,
            totalLecciones,
            totalPreguntas,
            totalEscenarios,
            totalEscenas,
            areasPorCompletados
        );
    }

    /// <inheritdoc />
    public async Task<bool> AlternarBloqueoIAUsuarioAsync(Guid usuarioId)
    {
        UsuarioApp? usuario = await _userManager.FindByIdAsync(usuarioId.ToString());
        if (usuario == null)
            throw new ExcepcionNoEncontrado("Usuario", usuarioId);

        usuario.EstaBloqueadoIA = !usuario.EstaBloqueadoIA;
        await _userManager.UpdateAsync(usuario);

        return usuario.EstaBloqueadoIA;
    }

    private async Task<IReadOnlyList<ActividadDiaria>> ObtenerActividadUltimos30DiasAsync()
    {
        DateTime hace30Dias = DateTime.UtcNow.AddDays(-30).Date;

        // Usuarios activos por día (por UltimoAcceso)
        List<ActividadDiaria> resultado = new List<ActividadDiaria>();

        for (int i = 0; i < 30; i++)
        {
            DateTime diaInicio = hace30Dias.AddDays(i);
            DateTime diaFin = diaInicio.AddDays(1);
            DateOnly fecha = DateOnly.FromDateTime(diaInicio);

            int usuariosActivos = await _userManager.Users
                .CountAsync(u => u.UltimoAcceso >= diaInicio && u.UltimoAcceso < diaFin);

            int leccionesCompletadas = await _contexto.ProgresosUsuario
                .CountAsync(p => p.Estado == EstadoProgreso.Completado
                    && p.FechaCompletado.HasValue
                    && p.FechaCompletado.Value >= diaInicio
                    && p.FechaCompletado.Value < diaFin);

            int mensajesIA = await _contexto.MensajesChatIA
                .CountAsync(m => m.FechaEnvio >= diaInicio && m.FechaEnvio < diaFin);

            resultado.Add(new ActividadDiaria(fecha, usuariosActivos, leccionesCompletadas, mensajesIA));
        }

        return resultado;
    }
}
