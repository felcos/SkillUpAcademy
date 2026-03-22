using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillUpAcademy.Core.DTOs.Escenas;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio para gestionar el motor de escenas del avatar Aria.
/// </summary>
public class ServicioEscenas : IServicioEscenas
{
    private readonly AppDbContext _contexto;
    private readonly ILogger<ServicioEscenas> _logger;

    /// <summary>
    /// Constructor del servicio de escenas.
    /// </summary>
    public ServicioEscenas(AppDbContext contexto, ILogger<ServicioEscenas> logger)
    {
        _contexto = contexto;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<EscenaDto>> ObtenerEscenasDeLeccionAsync(int leccionId)
    {
        List<EscenaLeccion> escenas = await _contexto.Set<EscenaLeccion>()
            .Include(e => e.Recursos)
            .Where(e => e.LeccionId == leccionId)
            .OrderBy(e => e.Orden)
            .ToListAsync();

        return escenas.Select(MapearADto).ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<EscenaDto>> GenerarEscenasAutomaticasAsync(int leccionId)
    {
        // Verificar si ya tiene escenas
        bool tieneEscenas = await _contexto.Set<EscenaLeccion>()
            .AnyAsync(e => e.LeccionId == leccionId);

        if (tieneEscenas)
        {
            _logger.LogDebug("La lección {LeccionId} ya tiene escenas, devolviéndolas", leccionId);
            return await ObtenerEscenasDeLeccionAsync(leccionId);
        }

        // Obtener la lección
        Leccion? leccion = await _contexto.Set<Leccion>()
            .FirstOrDefaultAsync(l => l.Id == leccionId);

        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        List<EscenaLeccion> escenasGeneradas = GenerarEscenasDesdeContenido(leccion);

        _contexto.Set<EscenaLeccion>().AddRange(escenasGeneradas);
        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Generadas {Cantidad} escenas automáticas para lección {LeccionId}",
            escenasGeneradas.Count, leccionId);

        return escenasGeneradas.Select(MapearADto).ToList();
    }

    /// <inheritdoc />
    public async Task<EscenaDto> ActualizarEscenaAsync(int escenaId, PeticionActualizarEscena peticion)
    {
        EscenaLeccion? escena = await _contexto.Set<EscenaLeccion>()
            .Include(e => e.Recursos)
            .FirstOrDefaultAsync(e => e.Id == escenaId);

        if (escena == null)
            throw new ExcepcionNoEncontrado("EscenaLeccion", escenaId);

        // Actualizar solo los campos que vienen en la petición (no null)
        if (!string.IsNullOrWhiteSpace(peticion.TipoContenidoVisual) &&
            Enum.TryParse<TipoContenidoVisual>(peticion.TipoContenidoVisual, true, out TipoContenidoVisual tipo))
        {
            escena.TipoContenidoVisual = tipo;
        }

        if (peticion.TituloEscena != null)
            escena.TituloEscena = peticion.TituloEscena;

        if (peticion.GuionAria != null)
            escena.GuionAria = peticion.GuionAria;

        if (peticion.ContenidoVisual != null)
            escena.ContenidoVisual = peticion.ContenidoVisual;

        if (peticion.MetadatosVisuales != null)
            escena.MetadatosVisuales = peticion.MetadatosVisuales;

        if (!string.IsNullOrWhiteSpace(peticion.TransicionEntrada) &&
            Enum.TryParse<TipoTransicion>(peticion.TransicionEntrada, true, out TipoTransicion transicion))
        {
            escena.TransicionEntrada = transicion;
        }

        if (!string.IsNullOrWhiteSpace(peticion.Layout) &&
            Enum.TryParse<TipoLayout>(peticion.Layout, true, out TipoLayout layout))
        {
            escena.Layout = layout;
        }

        if (peticion.DuracionSegundos.HasValue)
            escena.DuracionSegundos = peticion.DuracionSegundos.Value;

        if (peticion.EsPausaReflexiva.HasValue)
            escena.EsPausaReflexiva = peticion.EsPausaReflexiva.Value;

        if (peticion.SegundosPausa.HasValue)
            escena.SegundosPausa = peticion.SegundosPausa.Value;

        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Escena {EscenaId} actualizada", escenaId);
        return MapearADto(escena);
    }

    /// <inheritdoc />
    public async Task ReordenarEscenasAsync(int leccionId, IReadOnlyList<int> ordenEscenaIds)
    {
        List<EscenaLeccion> escenas = await _contexto.Set<EscenaLeccion>()
            .Where(e => e.LeccionId == leccionId)
            .ToListAsync();

        for (int i = 0; i < ordenEscenaIds.Count; i++)
        {
            EscenaLeccion? escena = escenas.FirstOrDefault(e => e.Id == ordenEscenaIds[i]);
            if (escena != null)
            {
                escena.Orden = i + 1;
            }
        }

        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Escenas de lección {LeccionId} reordenadas ({Cantidad} escenas)",
            leccionId, ordenEscenaIds.Count);
    }

    private static List<EscenaLeccion> GenerarEscenasDesdeContenido(Leccion leccion)
    {
        List<EscenaLeccion> escenas = new List<EscenaLeccion>();
        int orden = 1;

        // Escena 1: Introducción con el avatar solo
        escenas.Add(new EscenaLeccion
        {
            LeccionId = leccion.Id,
            Orden = orden++,
            TipoContenidoVisual = TipoContenidoVisual.Texto,
            TituloEscena = "Introducción",
            GuionAria = $"¡Hola! Hoy vamos a hablar sobre {leccion.Titulo}. {leccion.Descripcion ?? ""}",
            ContenidoVisual = leccion.Titulo,
            TransicionEntrada = TipoTransicion.Fade,
            Layout = TipoLayout.SoloAvatar,
            DuracionSegundos = 15
        });

        // Escenas 2-3: Contenido principal dividido
        if (!string.IsNullOrWhiteSpace(leccion.Contenido))
        {
            string[] secciones = DividirContenidoEnSecciones(leccion.Contenido);

            for (int i = 0; i < Math.Min(secciones.Length, 2); i++)
            {
                string seccion = secciones[i].Trim();
                if (string.IsNullOrWhiteSpace(seccion))
                    continue;

                // Extraer título de la sección si es un heading markdown
                string tituloSeccion = ExtraerTituloMarkdown(seccion) ?? $"Parte {i + 1}";
                string contenidoLimpio = LimpiarMarkdown(seccion);

                escenas.Add(new EscenaLeccion
                {
                    LeccionId = leccion.Id,
                    Orden = orden++,
                    TipoContenidoVisual = TipoContenidoVisual.Texto,
                    TituloEscena = tituloSeccion,
                    GuionAria = contenidoLimpio.Length > 500 ? contenidoLimpio[..500] : contenidoLimpio,
                    ContenidoVisual = seccion,
                    TransicionEntrada = i == 0 ? TipoTransicion.SlideLeft : TipoTransicion.Fade,
                    Layout = TipoLayout.AvatarYContenido,
                    DuracionSegundos = 30
                });
            }
        }

        // Escena 4: Puntos clave
        if (!string.IsNullOrWhiteSpace(leccion.PuntosClave))
        {
            escenas.Add(new EscenaLeccion
            {
                LeccionId = leccion.Id,
                Orden = orden++,
                TipoContenidoVisual = TipoContenidoVisual.ListaDePuntos,
                TituloEscena = "Puntos clave",
                GuionAria = "Repasemos los puntos más importantes de esta lección.",
                ContenidoVisual = leccion.PuntosClave,
                TransicionEntrada = TipoTransicion.ZoomIn,
                Layout = TipoLayout.AvatarYContenido,
                DuracionSegundos = 20,
                EsPausaReflexiva = true,
                SegundosPausa = 5
            });
        }

        // Escena 5: Cierre motivacional
        escenas.Add(new EscenaLeccion
        {
            LeccionId = leccion.Id,
            Orden = orden,
            TipoContenidoVisual = TipoContenidoVisual.Texto,
            TituloEscena = "Cierre",
            GuionAria = $"¡Excelente trabajo! Has completado la lección sobre {leccion.Titulo}. " +
                "Recuerda que la práctica constante es la clave del éxito. " +
                "Te animo a seguir adelante con la siguiente lección.",
            ContenidoVisual = "¡Bien hecho! Sigue adelante.",
            TransicionEntrada = TipoTransicion.Fade,
            Layout = TipoLayout.SoloAvatar,
            DuracionSegundos = 15
        });

        return escenas;
    }

    private static string[] DividirContenidoEnSecciones(string contenidoMarkdown)
    {
        // Dividir por headings de markdown (## o ###)
        string[] lineas = contenidoMarkdown.Split('\n');
        List<string> secciones = new List<string>();
        List<string> seccionActual = new List<string>();

        foreach (string linea in lineas)
        {
            if (linea.TrimStart().StartsWith("##") && seccionActual.Count > 0)
            {
                secciones.Add(string.Join('\n', seccionActual));
                seccionActual.Clear();
            }
            seccionActual.Add(linea);
        }

        if (seccionActual.Count > 0)
        {
            secciones.Add(string.Join('\n', seccionActual));
        }

        return secciones.ToArray();
    }

    private static string? ExtraerTituloMarkdown(string seccion)
    {
        string[] lineas = seccion.Split('\n');
        foreach (string linea in lineas)
        {
            string lineaTrimmed = linea.Trim();
            if (lineaTrimmed.StartsWith('#'))
            {
                return lineaTrimmed.TrimStart('#').Trim();
            }
        }
        return null;
    }

    private static string LimpiarMarkdown(string texto)
    {
        // Limpieza básica de markdown para convertir a texto plano para TTS
        string limpio = texto;
        limpio = System.Text.RegularExpressions.Regex.Replace(limpio, @"#{1,6}\s*", "");
        limpio = System.Text.RegularExpressions.Regex.Replace(limpio, @"\*{1,3}(.*?)\*{1,3}", "$1");
        limpio = System.Text.RegularExpressions.Regex.Replace(limpio, @"!\[.*?\]\(.*?\)", "");
        limpio = System.Text.RegularExpressions.Regex.Replace(limpio, @"\[([^\]]+)\]\(.*?\)", "$1");
        limpio = System.Text.RegularExpressions.Regex.Replace(limpio, @"`{1,3}[^`]*`{1,3}", "");
        limpio = System.Text.RegularExpressions.Regex.Replace(limpio, @"^[-*+]\s+", "", System.Text.RegularExpressions.RegexOptions.Multiline);
        limpio = System.Text.RegularExpressions.Regex.Replace(limpio, @"\n{3,}", "\n\n");
        return limpio.Trim();
    }

    private static EscenaDto MapearADto(EscenaLeccion escena)
    {
        return new EscenaDto
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
            Recursos = escena.Recursos?.Select(r => new RecursoVisualDto
            {
                Id = r.Id,
                TipoRecurso = r.TipoRecurso.ToString(),
                Nombre = r.Nombre,
                Url = r.Url,
                TextoAlternativo = r.TextoAlternativo
            }).ToList() ?? new List<RecursoVisualDto>()
        };
    }
}
