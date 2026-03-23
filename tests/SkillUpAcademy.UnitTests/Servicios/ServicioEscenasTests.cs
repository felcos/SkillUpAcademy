using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SkillUpAcademy.Core.DTOs.Escenas;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;
using SkillUpAcademy.Infrastructure.Servicios;

namespace SkillUpAcademy.UnitTests.Servicios;

/// <summary>
/// Tests unitarios para ServicioEscenas.
/// </summary>
public class ServicioEscenasTests : IDisposable
{
    private readonly AppDbContext _contexto;
    private readonly Mock<ILogger<ServicioEscenas>> _loggerMock;
    private readonly IServicioEscenas _servicio;

    public ServicioEscenasTests()
    {
        DbContextOptions<AppDbContext> opciones = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _contexto = new AppDbContext(opciones);
        _loggerMock = new Mock<ILogger<ServicioEscenas>>();

        _servicio = new ServicioEscenas(_contexto, _loggerMock.Object);
    }

    [Fact]
    public async Task ObtenerEscenasDeLeccionAsync_SinEscenas_DebeRetornarListaVacia()
    {
        // Act
        IReadOnlyList<EscenaDto> resultado = await _servicio.ObtenerEscenasDeLeccionAsync(999);

        // Assert
        resultado.Should().BeEmpty();
    }

    [Fact]
    public async Task ObtenerEscenasDeLeccionAsync_ConEscenas_DebeRetornarOrdenadasPorOrden()
    {
        // Arrange
        int leccionId = 1;
        _contexto.Set<EscenaLeccion>().AddRange(
            new EscenaLeccion
            {
                LeccionId = leccionId,
                Orden = 3,
                TipoContenidoVisual = TipoContenidoVisual.Texto,
                TituloEscena = "Tercera",
                Recursos = new List<RecursoVisual>()
            },
            new EscenaLeccion
            {
                LeccionId = leccionId,
                Orden = 1,
                TipoContenidoVisual = TipoContenidoVisual.Texto,
                TituloEscena = "Primera",
                Recursos = new List<RecursoVisual>()
            },
            new EscenaLeccion
            {
                LeccionId = leccionId,
                Orden = 2,
                TipoContenidoVisual = TipoContenidoVisual.ListaDePuntos,
                TituloEscena = "Segunda",
                Recursos = new List<RecursoVisual>()
            }
        );
        await _contexto.SaveChangesAsync();

        // Act
        IReadOnlyList<EscenaDto> resultado = await _servicio.ObtenerEscenasDeLeccionAsync(leccionId);

        // Assert
        resultado.Should().HaveCount(3);
        resultado[0].TituloEscena.Should().Be("Primera");
        resultado[1].TituloEscena.Should().Be("Segunda");
        resultado[2].TituloEscena.Should().Be("Tercera");
    }

    [Fact]
    public async Task GenerarEscenasAutomaticasAsync_LeccionNoExiste_DebeLanzarExcepcion()
    {
        // Act
        Func<Task> accion = async () => await _servicio.GenerarEscenasAutomaticasAsync(999);

        // Assert
        await accion.Should().ThrowAsync<ExcepcionNoEncontrado>();
    }

    [Fact]
    public async Task GenerarEscenasAutomaticasAsync_LeccionConContenido_DebeGenerar4o5Escenas()
    {
        // Arrange
        Leccion leccion = new Leccion
        {
            Id = 10,
            Titulo = "Comunicación Efectiva",
            Descripcion = "Aprende los fundamentos de la comunicación",
            Contenido = "## Sección 1\nContenido de la primera sección.\n## Sección 2\nContenido de la segunda sección.",
            PuntosClave = "Punto 1\nPunto 2\nPunto 3",
            TipoLeccion = TipoLeccion.Teoria,
            NivelId = 1,
            PuntosRecompensa = 10,
            Orden = 1
        };
        _contexto.Set<Leccion>().Add(leccion);
        await _contexto.SaveChangesAsync();

        // Act
        IReadOnlyList<EscenaDto> resultado = await _servicio.GenerarEscenasAutomaticasAsync(10);

        // Assert
        resultado.Should().HaveCountGreaterThanOrEqualTo(4);
        resultado[0].TituloEscena.Should().Be("Introducción");
        resultado[0].Layout.Should().Be("SoloAvatar");
        resultado.Last().TituloEscena.Should().Be("Cierre");

        // Debe haber una escena de puntos clave con pausa reflexiva
        resultado.Should().Contain(e => e.TipoContenidoVisual == "ListaDePuntos" && e.EsPausaReflexiva);
    }

    [Fact]
    public async Task GenerarEscenasAutomaticasAsync_YaTieneEscenas_DebeRetornarLasExistentes()
    {
        // Arrange
        int leccionId = 20;
        _contexto.Set<Leccion>().Add(new Leccion
        {
            Id = leccionId,
            Titulo = "Test",
            TipoLeccion = TipoLeccion.Teoria,
            NivelId = 1,
            PuntosRecompensa = 10,
            Orden = 1
        });
        _contexto.Set<EscenaLeccion>().Add(new EscenaLeccion
        {
            LeccionId = leccionId,
            Orden = 1,
            TipoContenidoVisual = TipoContenidoVisual.Texto,
            TituloEscena = "Existente",
            Recursos = new List<RecursoVisual>()
        });
        await _contexto.SaveChangesAsync();

        // Act
        IReadOnlyList<EscenaDto> resultado = await _servicio.GenerarEscenasAutomaticasAsync(leccionId);

        // Assert
        resultado.Should().HaveCount(1);
        resultado[0].TituloEscena.Should().Be("Existente");
    }

    [Fact]
    public async Task ActualizarEscenaAsync_EscenaNoExiste_DebeLanzarExcepcion()
    {
        // Arrange
        PeticionActualizarEscena peticion = new PeticionActualizarEscena
        {
            TituloEscena = "Nuevo título"
        };

        // Act
        Func<Task> accion = async () => await _servicio.ActualizarEscenaAsync(999, peticion);

        // Assert
        await accion.Should().ThrowAsync<ExcepcionNoEncontrado>();
    }

    [Fact]
    public async Task ActualizarEscenaAsync_ActualizacionParcial_SoloActualizaCamposNoNull()
    {
        // Arrange
        EscenaLeccion escena = new EscenaLeccion
        {
            LeccionId = 1,
            Orden = 1,
            TipoContenidoVisual = TipoContenidoVisual.Texto,
            TituloEscena = "Título original",
            GuionAria = "Guion original",
            ContenidoVisual = "Contenido original",
            DuracionSegundos = 15,
            Recursos = new List<RecursoVisual>()
        };
        _contexto.Set<EscenaLeccion>().Add(escena);
        await _contexto.SaveChangesAsync();

        PeticionActualizarEscena peticion = new PeticionActualizarEscena
        {
            TituloEscena = "Título actualizado",
            DuracionSegundos = 30
            // GuionAria y ContenidoVisual son null → no deben cambiar
        };

        // Act
        EscenaDto resultado = await _servicio.ActualizarEscenaAsync(escena.Id, peticion);

        // Assert
        resultado.TituloEscena.Should().Be("Título actualizado");
        resultado.DuracionSegundos.Should().Be(30);
        resultado.GuionAria.Should().Be("Guion original");
        resultado.ContenidoVisual.Should().Be("Contenido original");
    }

    [Fact]
    public async Task ActualizarEscenaAsync_CambiarTipoContenido_DebeActualizar()
    {
        // Arrange
        EscenaLeccion escena = new EscenaLeccion
        {
            LeccionId = 1,
            Orden = 1,
            TipoContenidoVisual = TipoContenidoVisual.Texto,
            TituloEscena = "Test",
            Recursos = new List<RecursoVisual>()
        };
        _contexto.Set<EscenaLeccion>().Add(escena);
        await _contexto.SaveChangesAsync();

        PeticionActualizarEscena peticion = new PeticionActualizarEscena
        {
            TipoContenidoVisual = "Diagrama",
            EsPausaReflexiva = true,
            SegundosPausa = 10
        };

        // Act
        EscenaDto resultado = await _servicio.ActualizarEscenaAsync(escena.Id, peticion);

        // Assert
        resultado.TipoContenidoVisual.Should().Be("Diagrama");
        resultado.EsPausaReflexiva.Should().BeTrue();
        resultado.SegundosPausa.Should().Be(10);
    }

    [Fact]
    public async Task ReordenarEscenasAsync_DebeAsignarNuevoOrden()
    {
        // Arrange
        int leccionId = 30;
        EscenaLeccion escena1 = new EscenaLeccion
        {
            LeccionId = leccionId, Orden = 1,
            TipoContenidoVisual = TipoContenidoVisual.Texto, TituloEscena = "A",
            Recursos = new List<RecursoVisual>()
        };
        EscenaLeccion escena2 = new EscenaLeccion
        {
            LeccionId = leccionId, Orden = 2,
            TipoContenidoVisual = TipoContenidoVisual.Texto, TituloEscena = "B",
            Recursos = new List<RecursoVisual>()
        };
        EscenaLeccion escena3 = new EscenaLeccion
        {
            LeccionId = leccionId, Orden = 3,
            TipoContenidoVisual = TipoContenidoVisual.Texto, TituloEscena = "C",
            Recursos = new List<RecursoVisual>()
        };
        _contexto.Set<EscenaLeccion>().AddRange(escena1, escena2, escena3);
        await _contexto.SaveChangesAsync();

        // Reordenar: C, A, B
        List<int> nuevoOrden = new List<int> { escena3.Id, escena1.Id, escena2.Id };

        // Act
        await _servicio.ReordenarEscenasAsync(leccionId, nuevoOrden);

        // Assert
        EscenaLeccion? escenaC = await _contexto.Set<EscenaLeccion>().FindAsync(escena3.Id);
        EscenaLeccion? escenaA = await _contexto.Set<EscenaLeccion>().FindAsync(escena1.Id);
        EscenaLeccion? escenaB = await _contexto.Set<EscenaLeccion>().FindAsync(escena2.Id);

        escenaC!.Orden.Should().Be(1);
        escenaA!.Orden.Should().Be(2);
        escenaB!.Orden.Should().Be(3);
    }

    [Fact]
    public async Task GenerarEscenasAutomaticasAsync_LeccionSinContenido_DebeGenerar2Escenas()
    {
        // Arrange
        Leccion leccion = new Leccion
        {
            Id = 40,
            Titulo = "Lección vacía",
            TipoLeccion = TipoLeccion.Teoria,
            NivelId = 1,
            PuntosRecompensa = 10,
            Orden = 1
            // Sin Contenido ni PuntosClave
        };
        _contexto.Set<Leccion>().Add(leccion);
        await _contexto.SaveChangesAsync();

        // Act
        IReadOnlyList<EscenaDto> resultado = await _servicio.GenerarEscenasAutomaticasAsync(40);

        // Assert
        resultado.Should().HaveCount(2); // Solo Introducción y Cierre
        resultado[0].TituloEscena.Should().Be("Introducción");
        resultado[1].TituloEscena.Should().Be("Cierre");
    }

    public void Dispose()
    {
        _contexto.Dispose();
    }
}
