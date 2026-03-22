using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.DTOs.Quiz;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de quizzes.
/// </summary>
public class ServicioQuiz(
    IRepositorioLecciones _repositorioLecciones,
    IRepositorioProgreso _repositorioProgreso,
    AppDbContext _contexto) : IServicioQuiz
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<PreguntaQuizDto>> ObtenerPreguntasAsync(int leccionId)
    {
        Leccion? leccion = await _repositorioLecciones.ObtenerConQuizAsync(leccionId);
        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        if (leccion.TipoLeccion != TipoLeccion.Quiz)
            throw new ExcepcionValidacion("Esta lección no es un quiz");

        List<PreguntaQuizDto> preguntas = new List<PreguntaQuizDto>();
        foreach (PreguntaQuiz pregunta in leccion.PreguntasQuiz)
        {
            preguntas.Add(new PreguntaQuizDto
            {
                Id = pregunta.Id,
                TextoPregunta = pregunta.TextoPregunta,
                Orden = pregunta.Orden,
                Opciones = pregunta.Opciones.Select(o => new OpcionQuizDto
                {
                    Id = o.Id,
                    TextoOpcion = o.TextoOpcion,
                    Orden = o.Orden
                }).ToList()
            });
        }

        return preguntas;
    }

    /// <inheritdoc />
    public async Task<RespuestaQuizDto> ResponderPreguntaAsync(int leccionId, PeticionRespuestaQuiz peticion, Guid usuarioId)
    {
        OpcionQuiz? opcionSeleccionada = await _contexto.OpcionesQuiz
            .Include(o => o.PreguntaQuiz)
            .FirstOrDefaultAsync(o => o.Id == peticion.OpcionSeleccionadaId);

        if (opcionSeleccionada == null)
            throw new ExcepcionNoEncontrado("OpcionQuiz", peticion.OpcionSeleccionadaId);

        OpcionQuiz? opcionCorrecta = await _contexto.OpcionesQuiz
            .FirstOrDefaultAsync(o => o.PreguntaQuizId == peticion.PreguntaId && o.EsCorrecta);

        // Guardar respuesta
        RespuestaQuizUsuario respuesta = new RespuestaQuizUsuario
        {
            UsuarioId = usuarioId,
            PreguntaQuizId = peticion.PreguntaId,
            OpcionSeleccionadaId = peticion.OpcionSeleccionadaId,
            EsCorrecta = opcionSeleccionada.EsCorrecta,
            FechaRespuesta = DateTime.UtcNow
        };
        _contexto.RespuestasQuizUsuario.Add(respuesta);
        await _contexto.SaveChangesAsync();

        return new RespuestaQuizDto
        {
            EsCorrecta = opcionSeleccionada.EsCorrecta,
            Retroalimentacion = opcionSeleccionada.Retroalimentacion,
            OpcionCorrectaId = opcionCorrecta?.Id ?? 0,
            Explicacion = opcionSeleccionada.PreguntaQuiz.Explicacion
        };
    }

    /// <inheritdoc />
    public async Task<ResultadoQuizDto> EnviarQuizCompletoAsync(int leccionId, PeticionQuizCompleto peticion, Guid usuarioId)
    {
        Leccion? leccion = await _repositorioLecciones.ObtenerConQuizAsync(leccionId);
        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        int correctas = 0;
        int total = leccion.PreguntasQuiz.Count;

        foreach (PeticionRespuestaQuiz respuestaPeticion in peticion.Respuestas)
        {
            OpcionQuiz? opcion = await _contexto.OpcionesQuiz
                .FirstOrDefaultAsync(o => o.Id == respuestaPeticion.OpcionSeleccionadaId);

            if (opcion != null && opcion.EsCorrecta)
                correctas++;

            // Guardar cada respuesta
            RespuestaQuizUsuario respuesta = new RespuestaQuizUsuario
            {
                UsuarioId = usuarioId,
                PreguntaQuizId = respuestaPeticion.PreguntaId,
                OpcionSeleccionadaId = respuestaPeticion.OpcionSeleccionadaId,
                EsCorrecta = opcion?.EsCorrecta ?? false,
                FechaRespuesta = DateTime.UtcNow
            };
            _contexto.RespuestasQuizUsuario.Add(respuesta);
        }

        await _contexto.SaveChangesAsync();

        int puntuacion = total > 0 ? (correctas * 100) / total : 0;
        bool aprobado = puntuacion >= 60;
        int puntosObtenidos = aprobado ? leccion.PuntosRecompensa : (leccion.PuntosRecompensa / 2);

        // Actualizar progreso
        ProgresoUsuario progreso = new ProgresoUsuario
        {
            UsuarioId = usuarioId,
            LeccionId = leccionId,
            Estado = aprobado ? EstadoProgreso.Completado : EstadoProgreso.EnProgreso,
            Puntuacion = puntosObtenidos,
            FechaCompletado = aprobado ? DateTime.UtcNow : null,
            UltimoAcceso = DateTime.UtcNow
        };
        await _repositorioProgreso.CrearOActualizarAsync(progreso);

        return new ResultadoQuizDto
        {
            PreguntasTotales = total,
            RespuestasCorrectas = correctas,
            Puntuacion = puntuacion,
            PuntosObtenidos = puntosObtenidos,
            Aprobado = aprobado,
            PuntuacionMinima = 60
        };
    }
}
