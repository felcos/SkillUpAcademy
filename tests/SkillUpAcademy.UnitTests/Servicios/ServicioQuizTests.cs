using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SkillUpAcademy.Core.DTOs.Quiz;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;
using SkillUpAcademy.Infrastructure.Servicios;

namespace SkillUpAcademy.UnitTests.Servicios;

/// <summary>
/// Tests unitarios para ServicioQuiz.
/// </summary>
public class ServicioQuizTests : IDisposable
{
    private readonly AppDbContext _contexto;
    private readonly Mock<IRepositorioLecciones> _repositorioLeccionesMock;
    private readonly Mock<IRepositorioProgreso> _repositorioProgresoMock;
    private readonly IServicioQuiz _servicio;

    public ServicioQuizTests()
    {
        DbContextOptions<AppDbContext> opciones = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _contexto = new AppDbContext(opciones);
        _repositorioLeccionesMock = new Mock<IRepositorioLecciones>();
        _repositorioProgresoMock = new Mock<IRepositorioProgreso>();

        _servicio = new ServicioQuiz(
            _repositorioLeccionesMock.Object,
            _repositorioProgresoMock.Object,
            _contexto);
    }

    [Fact]
    public async Task ObtenerPreguntasAsync_LeccionExistente_DebeRetornarPreguntas()
    {
        // Arrange
        int leccionId = 1;
        Leccion leccion = new Leccion
        {
            Id = leccionId,
            Titulo = "Quiz de prueba",
            TipoLeccion = TipoLeccion.Quiz,
            PreguntasQuiz = new List<PreguntaQuiz>
            {
                new PreguntaQuiz
                {
                    Id = 1, LeccionId = leccionId, TextoPregunta = "¿Pregunta 1?", Orden = 1,
                    Opciones = new List<OpcionQuiz>
                    {
                        new OpcionQuiz { Id = 1, TextoOpcion = "Opción A", EsCorrecta = true, Orden = 1 },
                        new OpcionQuiz { Id = 2, TextoOpcion = "Opción B", EsCorrecta = false, Orden = 2 }
                    }
                }
            }
        };

        _repositorioLeccionesMock
            .Setup(r => r.ObtenerConQuizAsync(leccionId))
            .ReturnsAsync(leccion);

        // Act
        IReadOnlyList<PreguntaQuizDto> resultado = await _servicio.ObtenerPreguntasAsync(leccionId);

        // Assert
        resultado.Should().HaveCount(1);
        resultado[0].TextoPregunta.Should().Be("¿Pregunta 1?");
        resultado[0].Opciones.Should().HaveCount(2);
    }

    [Fact]
    public async Task ObtenerPreguntasAsync_LeccionNoExiste_DebeLanzarExcepcion()
    {
        // Arrange
        _repositorioLeccionesMock
            .Setup(r => r.ObtenerConQuizAsync(It.IsAny<int>()))
            .ReturnsAsync((Leccion?)null);

        // Act
        Func<Task> accion = async () => await _servicio.ObtenerPreguntasAsync(999);

        // Assert
        await accion.Should().ThrowAsync<ExcepcionNoEncontrado>();
    }

    [Fact]
    public async Task ObtenerPreguntasAsync_LeccionNoEsQuiz_DebeLanzarExcepcion()
    {
        // Arrange
        Leccion leccion = new Leccion
        {
            Id = 1, Titulo = "Teoría", TipoLeccion = TipoLeccion.Teoria
        };

        _repositorioLeccionesMock
            .Setup(r => r.ObtenerConQuizAsync(1))
            .ReturnsAsync(leccion);

        // Act
        Func<Task> accion = async () => await _servicio.ObtenerPreguntasAsync(1);

        // Assert
        await accion.Should().ThrowAsync<ExcepcionValidacion>();
    }

    [Fact]
    public async Task EnviarQuizCompletoAsync_TodoCorrecto_DebeDar100Porciento()
    {
        // Arrange
        int leccionId = 1;
        Guid usuarioId = Guid.NewGuid();

        // Añadir opciones al InMemory DB para que el servicio las encuentre
        OpcionQuiz opcionCorrecta1 = new OpcionQuiz { Id = 10, PreguntaQuizId = 1, TextoOpcion = "Correcta", EsCorrecta = true, Retroalimentacion = "¡Bien!", Orden = 1 };
        OpcionQuiz opcionIncorrecta1 = new OpcionQuiz { Id = 11, PreguntaQuizId = 1, TextoOpcion = "Incorrecta", EsCorrecta = false, Retroalimentacion = "Mal.", Orden = 2 };
        OpcionQuiz opcionCorrecta2 = new OpcionQuiz { Id = 20, PreguntaQuizId = 2, TextoOpcion = "Correcta", EsCorrecta = true, Retroalimentacion = "¡Bien!", Orden = 1 };
        OpcionQuiz opcionIncorrecta2 = new OpcionQuiz { Id = 21, PreguntaQuizId = 2, TextoOpcion = "Incorrecta", EsCorrecta = false, Retroalimentacion = "Mal.", Orden = 2 };

        _contexto.OpcionesQuiz.AddRange(opcionCorrecta1, opcionIncorrecta1, opcionCorrecta2, opcionIncorrecta2);
        await _contexto.SaveChangesAsync();

        Leccion leccion = new Leccion
        {
            Id = leccionId,
            Titulo = "Quiz",
            PuntosRecompensa = 15,
            PreguntasQuiz = new List<PreguntaQuiz>
            {
                new PreguntaQuiz { Id = 1, LeccionId = leccionId, TextoPregunta = "¿P1?", Orden = 1, Opciones = new List<OpcionQuiz> { opcionCorrecta1, opcionIncorrecta1 } },
                new PreguntaQuiz { Id = 2, LeccionId = leccionId, TextoPregunta = "¿P2?", Orden = 2, Opciones = new List<OpcionQuiz> { opcionCorrecta2, opcionIncorrecta2 } }
            }
        };

        _repositorioLeccionesMock
            .Setup(r => r.ObtenerConQuizAsync(leccionId))
            .ReturnsAsync(leccion);

        _repositorioProgresoMock
            .Setup(r => r.CrearOActualizarAsync(It.IsAny<ProgresoUsuario>()))
            .ReturnsAsync((ProgresoUsuario p) => p);

        PeticionQuizCompleto peticion = new PeticionQuizCompleto
        {
            Respuestas = new List<PeticionRespuestaQuiz>
            {
                new PeticionRespuestaQuiz(1, 10),
                new PeticionRespuestaQuiz(2, 20)
            }
        };

        // Act
        ResultadoQuizDto resultado = await _servicio.EnviarQuizCompletoAsync(leccionId, peticion, usuarioId);

        // Assert
        resultado.Puntuacion.Should().Be(100);
        resultado.Aprobado.Should().BeTrue();
        resultado.RespuestasCorrectas.Should().Be(2);
        resultado.PreguntasTotales.Should().Be(2);
    }

    [Fact]
    public async Task EnviarQuizCompletoAsync_TodoIncorrecto_NoDebeAprobar()
    {
        // Arrange
        int leccionId = 2;
        Guid usuarioId = Guid.NewGuid();

        OpcionQuiz opcionCorrecta = new OpcionQuiz { Id = 30, PreguntaQuizId = 3, TextoOpcion = "Correcta", EsCorrecta = true, Orden = 1 };
        OpcionQuiz opcionIncorrecta = new OpcionQuiz { Id = 31, PreguntaQuizId = 3, TextoOpcion = "Incorrecta", EsCorrecta = false, Orden = 2 };

        _contexto.OpcionesQuiz.AddRange(opcionCorrecta, opcionIncorrecta);
        await _contexto.SaveChangesAsync();

        Leccion leccion = new Leccion
        {
            Id = leccionId,
            Titulo = "Quiz 2",
            PuntosRecompensa = 15,
            PreguntasQuiz = new List<PreguntaQuiz>
            {
                new PreguntaQuiz { Id = 3, LeccionId = leccionId, TextoPregunta = "¿P1?", Orden = 1, Opciones = new List<OpcionQuiz> { opcionCorrecta, opcionIncorrecta } }
            }
        };

        _repositorioLeccionesMock
            .Setup(r => r.ObtenerConQuizAsync(leccionId))
            .ReturnsAsync(leccion);

        _repositorioProgresoMock
            .Setup(r => r.CrearOActualizarAsync(It.IsAny<ProgresoUsuario>()))
            .ReturnsAsync((ProgresoUsuario p) => p);

        PeticionQuizCompleto peticion = new PeticionQuizCompleto
        {
            Respuestas = new List<PeticionRespuestaQuiz>
            {
                new PeticionRespuestaQuiz(3, 31)
            }
        };

        // Act
        ResultadoQuizDto resultado = await _servicio.EnviarQuizCompletoAsync(leccionId, peticion, usuarioId);

        // Assert
        resultado.Puntuacion.Should().Be(0);
        resultado.Aprobado.Should().BeFalse();
    }

    public void Dispose()
    {
        _contexto.Dispose();
    }
}
