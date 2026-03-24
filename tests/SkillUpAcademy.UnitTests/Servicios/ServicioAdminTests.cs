using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkillUpAcademy.Core.DTOs.Admin;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;
using SkillUpAcademy.Infrastructure.Servicios;

namespace SkillUpAcademy.UnitTests.Servicios;

/// <summary>
/// Tests unitarios para ServicioAdmin.
/// Usa SQLite in-memory porque InMemory no soporta las queries LINQ complejas
/// con SelectMany anidados que usa ObtenerEstadisticasContenidoAsync.
/// </summary>
public class ServicioAdminTests : IDisposable
{
    private readonly SqliteConnection _conexionSqlite;
    private readonly AppDbContext _contexto;
    private readonly UserManager<UsuarioApp> _userManager;
    private readonly IServicioAdmin _servicio;
    private readonly ServiceProvider _serviceProvider;

    public ServicioAdminTests()
    {
        _conexionSqlite = new SqliteConnection("DataSource=:memory:");
        _conexionSqlite.Open();

        ServiceCollection services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(opciones =>
            opciones.UseSqlite(_conexionSqlite));

        services.AddIdentityCore<UsuarioApp>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddLogging();

        _serviceProvider = services.BuildServiceProvider();
        _contexto = _serviceProvider.GetRequiredService<AppDbContext>();
        _contexto.Database.EnsureCreated();

        _userManager = _serviceProvider.GetRequiredService<UserManager<UsuarioApp>>();

        _servicio = new ServicioAdmin(_contexto, _userManager);
    }

    [Fact]
    public async Task ObtenerResumenAsync_DebeRetornarResumen()
    {
        // Arrange
        UsuarioApp usuario1 = new UsuarioApp
        {
            Id = Guid.NewGuid(),
            UserName = "usuario1@test.com",
            Email = "usuario1@test.com",
            Nombre = "Test",
            Apellidos = "Uno",
            UltimoAcceso = DateTime.UtcNow.AddDays(-1)
        };
        UsuarioApp usuario2 = new UsuarioApp
        {
            Id = Guid.NewGuid(),
            UserName = "usuario2@test.com",
            Email = "usuario2@test.com",
            Nombre = "Test",
            Apellidos = "Dos",
            UltimoAcceso = DateTime.UtcNow.AddDays(-10)
        };
        await _userManager.CreateAsync(usuario1, "Password1!");
        await _userManager.CreateAsync(usuario2, "Password2!");

        SesionChatIA sesion = new SesionChatIA
        {
            Id = Guid.NewGuid(),
            UsuarioId = usuario1.Id,
            Activa = true
        };
        _contexto.SesionesChatIA.Add(sesion);

        _contexto.MensajesChatIA.Add(new MensajeChatIA
        {
            Rol = "user",
            Contenido = "Hola",
            FechaEnvio = DateTime.UtcNow,
            SesionId = sesion.Id
        });
        await _contexto.SaveChangesAsync();

        // Act
        ResumenAdmin resultado = await _servicio.ObtenerResumenAsync();

        // Assert
        resultado.TotalUsuarios.Should().Be(2);
        resultado.UsuariosActivos7Dias.Should().Be(1);
        resultado.TotalSesionesIA.Should().Be(1);
        resultado.TotalMensajesIA.Should().Be(1);
        resultado.ActividadUltimos30Dias.Should().HaveCount(30);
    }

    [Fact]
    public async Task ObtenerUsuariosAsync_PrimeraPagina_DebeRetornarUsuarios()
    {
        // Arrange
        UsuarioApp usuario1 = new UsuarioApp
        {
            Id = Guid.NewGuid(),
            UserName = "pag1@test.com",
            Email = "pag1@test.com",
            Nombre = "Ana",
            Apellidos = "García",
            FechaCreacion = DateTime.UtcNow.AddDays(-5)
        };
        UsuarioApp usuario2 = new UsuarioApp
        {
            Id = Guid.NewGuid(),
            UserName = "pag2@test.com",
            Email = "pag2@test.com",
            Nombre = "Carlos",
            Apellidos = "López",
            FechaCreacion = DateTime.UtcNow.AddDays(-1)
        };
        await _userManager.CreateAsync(usuario1, "Password1!");
        await _userManager.CreateAsync(usuario2, "Password2!");

        // Act
        IReadOnlyList<UsuarioAdmin> resultado = await _servicio.ObtenerUsuariosAsync(1, 10);

        // Assert
        resultado.Should().HaveCount(2);
        resultado[0].NombreCompleto.Should().Be("Carlos López");
        resultado[1].NombreCompleto.Should().Be("Ana García");
    }

    [Fact]
    public async Task ObtenerTotalUsuariosAsync_DebeRetornarConteo()
    {
        // Arrange
        await _userManager.CreateAsync(new UsuarioApp
        {
            Id = Guid.NewGuid(),
            UserName = "total1@test.com",
            Email = "total1@test.com",
            Nombre = "Test",
            Apellidos = "Uno"
        }, "Password1!");

        await _userManager.CreateAsync(new UsuarioApp
        {
            Id = Guid.NewGuid(),
            UserName = "total2@test.com",
            Email = "total2@test.com",
            Nombre = "Test",
            Apellidos = "Dos"
        }, "Password2!");

        await _userManager.CreateAsync(new UsuarioApp
        {
            Id = Guid.NewGuid(),
            UserName = "total3@test.com",
            Email = "total3@test.com",
            Nombre = "Test",
            Apellidos = "Tres"
        }, "Password3!");

        // Act
        int resultado = await _servicio.ObtenerTotalUsuariosAsync();

        // Assert
        resultado.Should().Be(3);
    }

    [Fact(Skip = "La query LINQ compleja de AreasPorCompletados con record constructor en Select " +
        "no es compatible con SQLite ni InMemory (bug de EF Core 8). Requiere PostgreSQL real.")]
    public async Task ObtenerEstadisticasContenidoAsync_DebeRetornarEstadisticas()
    {
        // Arrange
        AreaHabilidad area = new AreaHabilidad
        {
            Titulo = "Liderazgo",
            Descripcion = "Habilidades de liderazgo",
            Activo = true,
            Orden = 1,
            Icono = "icon"
        };
        _contexto.AreasHabilidad.Add(area);
        await _contexto.SaveChangesAsync();

        Nivel nivel = new Nivel
        {
            Nombre = "Básico",
            Descripcion = "Nivel básico",
            Activo = true,
            NumeroNivel = 1,
            AreaHabilidadId = area.Id
        };
        _contexto.Niveles.Add(nivel);
        await _contexto.SaveChangesAsync();

        Leccion leccion = new Leccion
        {
            Titulo = "Lección 1",
            TipoLeccion = TipoLeccion.Teoria,
            NivelId = nivel.Id,
            PuntosRecompensa = 10,
            Orden = 1,
            Activo = true
        };
        _contexto.Lecciones.Add(leccion);
        await _contexto.SaveChangesAsync();

        // Act
        EstadisticasContenido resultado = await _servicio.ObtenerEstadisticasContenidoAsync();

        // Assert
        resultado.TotalAreas.Should().Be(1);
        resultado.TotalNiveles.Should().Be(1);
        resultado.TotalLecciones.Should().Be(1);
        resultado.AreasPorCompletados.Should().HaveCount(1);
        resultado.AreasPorCompletados[0].NombreArea.Should().Be("Liderazgo");
    }

    [Fact]
    public async Task AlternarBloqueoIAUsuarioAsync_UsuarioExiste_DebeCambiarEstado()
    {
        // Arrange
        Guid usuarioId = Guid.NewGuid();
        UsuarioApp usuario = new UsuarioApp
        {
            Id = usuarioId,
            UserName = "bloqueo@test.com",
            Email = "bloqueo@test.com",
            Nombre = "Test",
            Apellidos = "Bloqueo",
            EstaBloqueadoIA = false
        };
        await _userManager.CreateAsync(usuario, "Password1!");

        // Act
        bool primerResultado = await _servicio.AlternarBloqueoIAUsuarioAsync(usuarioId);

        // Assert
        primerResultado.Should().BeTrue();
    }

    [Fact]
    public async Task AlternarBloqueoIAUsuarioAsync_UsuarioNoExiste_DebeLanzarExcepcion()
    {
        // Arrange
        Guid usuarioIdInexistente = Guid.NewGuid();

        // Act
        Func<Task> accion = async () => await _servicio.AlternarBloqueoIAUsuarioAsync(usuarioIdInexistente);

        // Assert
        await accion.Should().ThrowAsync<ExcepcionNoEncontrado>();
    }

    public void Dispose()
    {
        _contexto.Dispose();
        _serviceProvider.Dispose();
        _conexionSqlite.Dispose();
    }
}
