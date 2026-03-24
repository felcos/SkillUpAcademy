using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SkillUpAcademy.Core.DTOs.Tts;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Infrastructure.Datos;
using SkillUpAcademy.Infrastructure.Servicios;

namespace SkillUpAcademy.UnitTests.Servicios;

/// <summary>
/// Tests unitarios para ServicioTts multi-proveedor.
/// </summary>
public class ServicioTtsTests : IDisposable
{
    private readonly SqliteConnection _conexionSqlite;
    private readonly AppDbContext _contexto;
    private readonly UserManager<UsuarioApp> _userManager;
    private readonly ServicioTts _servicio;
    private readonly ServiceProvider _serviceProvider;

    public ServicioTtsTests()
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

        Mock<ILogger<ServicioTts>> loggerMock = new Mock<ILogger<ServicioTts>>();
        HttpClient httpClient = new HttpClient();

        _servicio = new ServicioTts(_contexto, _userManager, loggerMock.Object, httpClient);
    }

    [Fact]
    public async Task EstaDisponibleAsync_SinProveedoresHabilitados_DebeRetornarFalse()
    {
        // Act
        bool disponible = await _servicio.EstaDisponibleAsync();

        // Assert
        disponible.Should().BeFalse();
    }

    [Fact]
    public async Task EstaDisponibleAsync_ConProveedorHabilitadoYApiKey_DebeRetornarTrue()
    {
        // Arrange
        _contexto.ProveedoresTts.Add(new ProveedorTts
        {
            Tipo = TipoProveedorTts.AzureSpeech,
            NombreVisible = "Azure",
            Habilitado = true,
            ApiKey = "mi-clave",
            VozPorDefecto = "es-ES-ElviraNeural",
            Orden = 1
        });
        await _contexto.SaveChangesAsync();

        // Act
        bool disponible = await _servicio.EstaDisponibleAsync();

        // Assert
        disponible.Should().BeTrue();
    }

    [Fact]
    public async Task GenerarAudioAsync_SinProveedores_DebeRetornarArrayVacio()
    {
        // Act
        byte[] audio = await _servicio.GenerarAudioAsync("Hola mundo");

        // Assert
        audio.Should().BeEmpty();
    }

    [Fact]
    public async Task GenerarAudioAsync_ProveedorWebSpeech_DebeRetornarArrayVacio()
    {
        // Act — forzar WebSpeechApi como proveedor
        byte[] audio = await _servicio.GenerarAudioAsync("Hola mundo", proveedor: "WebSpeechApi");

        // Assert — Web Speech se maneja en frontend, backend devuelve vacío
        audio.Should().BeEmpty();
    }

    [Fact]
    public async Task ObtenerVocesDisponiblesAsync_SinProveedores_DebeRetornarSoloWebSpeech()
    {
        // Act
        IReadOnlyList<VozDisponibleDto> voces = await _servicio.ObtenerVocesDisponiblesAsync();

        // Assert — solo la voz del sistema
        voces.Should().HaveCount(1);
        voces[0].Proveedor.Should().Be("WebSpeechApi");
    }

    [Fact]
    public async Task ObtenerVocesDisponiblesAsync_ConAzureHabilitado_DebeIncluirVocesAzure()
    {
        // Arrange
        _contexto.ProveedoresTts.Add(new ProveedorTts
        {
            Tipo = TipoProveedorTts.AzureSpeech,
            NombreVisible = "Azure",
            Habilitado = true,
            VozPorDefecto = "es-ES-ElviraNeural",
            Orden = 1
        });
        await _contexto.SaveChangesAsync();

        // Act
        IReadOnlyList<VozDisponibleDto> voces = await _servicio.ObtenerVocesDisponiblesAsync();

        // Assert — voces Azure + voz del sistema
        voces.Should().HaveCountGreaterThan(1);
        voces.Should().Contain(v => v.Proveedor == "AzureSpeech");
        voces.Should().Contain(v => v.Proveedor == "WebSpeechApi");
    }

    [Fact]
    public async Task ObtenerConfiguracionUsuarioAsync_UsuarioExistente_DebeRetornarConfiguracion()
    {
        // Arrange
        UsuarioApp usuario = new UsuarioApp
        {
            UserName = "test@test.com",
            Email = "test@test.com",
            Nombre = "Test",
            Apellidos = "User",
            VozPreferida = "es-ES-ElviraNeural",
            VelocidadVoz = 1.25m,
            ProveedorTtsPreferido = "AzureSpeech"
        };
        await _userManager.CreateAsync(usuario, "Test1234!");

        // Act
        ConfiguracionTtsUsuarioDto config = await _servicio.ObtenerConfiguracionUsuarioAsync(usuario.Id);

        // Assert
        config.VozSeleccionada.Should().Be("es-ES-ElviraNeural");
        config.VelocidadVoz.Should().Be(1.25m);
        config.ProveedorPreferido.Should().Be("AzureSpeech");
        config.Proveedores.Should().Contain(p => p.Tipo == "WebSpeechApi"); // Siempre incluido
    }

    [Fact]
    public async Task ObtenerConfiguracionUsuarioAsync_UsuarioInexistente_DebeLanzarExcepcion()
    {
        // Act
        Func<Task> accion = () => _servicio.ObtenerConfiguracionUsuarioAsync(Guid.NewGuid());

        // Assert
        await accion.Should().ThrowAsync<ExcepcionNoEncontrado>();
    }

    [Fact]
    public async Task ActualizarPreferenciaVozAsync_DebeActualizarUsuario()
    {
        // Arrange
        UsuarioApp usuario = new UsuarioApp
        {
            UserName = "voz@test.com",
            Email = "voz@test.com",
            Nombre = "Voz",
            Apellidos = "Test"
        };
        await _userManager.CreateAsync(usuario, "Test1234!");

        PeticionActualizarVoz peticion = new PeticionActualizarVoz
        {
            VozPreferida = "es-MX-DaliaNeural",
            VelocidadVoz = 1.5m,
            ProveedorPreferido = "AzureSpeech"
        };

        // Act
        await _servicio.ActualizarPreferenciaVozAsync(usuario.Id, peticion);

        // Assert
        UsuarioApp? actualizado = await _userManager.FindByIdAsync(usuario.Id.ToString());
        actualizado!.VozPreferida.Should().Be("es-MX-DaliaNeural");
        actualizado.VelocidadVoz.Should().Be(1.5m);
        actualizado.ProveedorTtsPreferido.Should().Be("AzureSpeech");
    }

    [Fact]
    public async Task ActualizarPreferenciaVozAsync_VelocidadFueraDeLimites_DebeClamp()
    {
        // Arrange
        UsuarioApp usuario = new UsuarioApp
        {
            UserName = "speed@test.com",
            Email = "speed@test.com",
            Nombre = "Speed",
            Apellidos = "Test"
        };
        await _userManager.CreateAsync(usuario, "Test1234!");

        // Act — velocidad demasiado alta
        await _servicio.ActualizarPreferenciaVozAsync(usuario.Id, new PeticionActualizarVoz { VelocidadVoz = 5.0m });

        // Assert — clamped a 2.0
        UsuarioApp? actualizado = await _userManager.FindByIdAsync(usuario.Id.ToString());
        actualizado!.VelocidadVoz.Should().Be(2.0m);
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
        _conexionSqlite.Dispose();
    }
}
