using System.Text.Json;
using SkillUpAcademy.Core.DTOs.Escenas;
using SkillUpAcademy.Core.DTOs.Habilidades;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de gestión de lecciones.
/// </summary>
public class ServicioLecciones(
    IRepositorioLecciones _repositorioLecciones,
    IRepositorioProgreso _repositorioProgreso,
    IServicioProgreso _servicioProgreso) : IServicioLecciones
{
    /// <inheritdoc />
    public async Task<LeccionDetalleDto> ObtenerLeccionAsync(int leccionId, Guid usuarioId)
    {
        Leccion? leccion = await _repositorioLecciones.ObtenerConEscenasAsync(leccionId);
        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        ProgresoUsuario? progreso = await _repositorioProgreso.ObtenerAsync(usuarioId, leccionId);

        List<string> puntosClave = new List<string>();
        if (!string.IsNullOrWhiteSpace(leccion.PuntosClave))
        {
            try
            {
                List<string>? deserializado = JsonSerializer.Deserialize<List<string>>(leccion.PuntosClave);
                if (deserializado != null)
                    puntosClave = deserializado;
            }
            catch (JsonException)
            {
                // Si el JSON es inválido, dejamos la lista vacía
            }
        }

        return new LeccionDetalleDto
        {
            Id = leccion.Id,
            TipoLeccion = leccion.TipoLeccion.ToString(),
            Titulo = leccion.Titulo,
            Descripcion = leccion.Descripcion,
            Contenido = leccion.Contenido,
            PuntosClave = puntosClave,
            GuionAudio = leccion.GuionAudio,
            PuntosRecompensa = leccion.PuntosRecompensa,
            DuracionMinutos = leccion.DuracionMinutos,
            Estado = progreso?.Estado.ToString() ?? "NoIniciado",
            TieneEscenas = leccion.Escenas.Count > 0
        };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<EscenaDto>> ObtenerEscenasAsync(int leccionId)
    {
        Leccion? leccion = await _repositorioLecciones.ObtenerConEscenasAsync(leccionId);
        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        List<EscenaDto> escenas = new List<EscenaDto>();
        foreach (EscenaLeccion escena in leccion.Escenas)
        {
            escenas.Add(new EscenaDto
            {
                Id = escena.Id,
                Orden = escena.Orden,
                TipoContenidoVisual = escena.TipoContenidoVisual.ToString(),
                TituloEscena = escena.TituloEscena,
                GuionAria = escena.GuionAria,
                ContenidoVisual = escena.ContenidoVisual,
                MetadatosVisuales = escena.MetadatosVisuales,
                TransicionEntrada = escena.TransicionEntrada.ToString(),
                Layout = escena.Layout.ToString(),
                DuracionSegundos = escena.DuracionSegundos,
                EsPausaReflexiva = escena.EsPausaReflexiva,
                SegundosPausa = escena.SegundosPausa,
                Recursos = escena.Recursos.Select(r => new RecursoVisualDto
                {
                    Id = r.Id,
                    TipoRecurso = r.TipoRecurso.ToString(),
                    Nombre = r.Nombre,
                    Url = r.Url,
                    TextoAlternativo = r.TextoAlternativo
                }).ToList()
            });
        }

        return escenas;
    }

    /// <inheritdoc />
    public async Task IniciarLeccionAsync(int leccionId, Guid usuarioId)
    {
        Leccion? leccion = await _repositorioLecciones.ObtenerPorIdAsync(leccionId);
        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        ProgresoUsuario progreso = new ProgresoUsuario
        {
            UsuarioId = usuarioId,
            LeccionId = leccionId,
            Estado = EstadoProgreso.EnProgreso,
            UltimoAcceso = DateTime.UtcNow
        };

        await _repositorioProgreso.CrearOActualizarAsync(progreso);
        await _servicioProgreso.ActualizarRachaAsync(usuarioId);
    }

    /// <inheritdoc />
    public async Task<ResultadoCompletarLeccion> CompletarLeccionAsync(int leccionId, Guid usuarioId)
    {
        Leccion? leccion = await _repositorioLecciones.ObtenerPorIdAsync(leccionId);
        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        ProgresoUsuario progreso = new ProgresoUsuario
        {
            UsuarioId = usuarioId,
            LeccionId = leccionId,
            Estado = EstadoProgreso.Completado,
            Puntuacion = leccion.PuntosRecompensa,
            FechaCompletado = DateTime.UtcNow,
            UltimoAcceso = DateTime.UtcNow
        };

        await _repositorioProgreso.CrearOActualizarAsync(progreso);

        // Verificar logros
        IReadOnlyList<Core.DTOs.Progreso.LogroDto> logrosNuevos = await _servicioProgreso.VerificarLogrosAsync(usuarioId);

        return new ResultadoCompletarLeccion
        {
            PuntosObtenidos = leccion.PuntosRecompensa,
            PuntosTotalesUsuario = 0, // Se actualizará con el progreso real
            LogrosDesbloqueados = logrosNuevos.Select(l => l.Titulo).ToList()
        };
    }
}
