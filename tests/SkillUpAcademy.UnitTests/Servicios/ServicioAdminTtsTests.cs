using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
/// Tests unitarios para ServicioAdminTts.
/// </summary>
public class ServicioAdminTtsTests : IDisposable
{
    private readonly SqliteConnection _conexionSqlite;
    private readonly AppDbContext _contexto;
    private readonly ServicioAdminTts _servicio;

    public ServicioAdminTtsTests()
    {
        _conexionSqlite = new SqliteConnection("DataSource=:memory:");
        _conexionSqlite.Open();

        DbContextOptions<AppDbContext> opciones = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_conexionSqlite)
            .Options;

        _contexto = new AppDbContext(opciones);
        _contexto.Database.EnsureCreated();

        Mock<ILogger<ServicioAdminTts>> loggerMock = new Mock<ILogger<ServicioAdminTts>>();

        _servicio = new ServicioAdminTts(_contexto, loggerMock.Object);

        // Sembrar proveedores de prueba
        _contexto.ProveedoresTts.AddRange(
            new ProveedorTts
            {
                Tipo = TipoProveedorTts.AzureSpeech,
                NombreVisible = "Azure Test",
                Habilitado = false,
                VozPorDefecto = "es-ES-ElviraNeural",
                Region = "westeurope",
                Orden = 1
            },
            new ProveedorTts
            {
                Tipo = TipoProveedorTts.ElevenLabs,
                NombreVisible = "ElevenLabs Test",
                Habilitado = false,
                VozPorDefecto = "21m00Tcm4TlvDq8ikWAM",
                Orden = 2
            }
        );
        _contexto.SaveChanges();
    }

    [Fact]
    public async Task ObtenerProveedoresAsync_DebeRetornarTodosLosProveedores()
    {
        // Act
        IReadOnlyList<ConfiguracionProveedorTtsDto> resultado = await _servicio.ObtenerProveedoresAsync();

        // Assert
        resultado.Should().HaveCount(2);
        resultado[0].Tipo.Should().Be("AzureSpeech");
        resultado[1].Tipo.Should().Be("ElevenLabs");
    }

    [Fact]
    public async Task ObtenerProveedorAsync_ProveedorExistente_DebeRetornarProveedor()
    {
        // Act
        ConfiguracionProveedorTtsDto resultado = await _servicio.ObtenerProveedorAsync(TipoProveedorTts.AzureSpeech);

        // Assert
        resultado.NombreVisible.Should().Be("Azure Test");
        resultado.Habilitado.Should().BeFalse();
        resultado.TieneApiKey.Should().BeFalse();
        resultado.Region.Should().Be("westeurope");
    }

    [Fact]
    public async Task ObtenerProveedorAsync_ProveedorInexistente_DebeLanzarExcepcion()
    {
        // Act
        Func<Task> accion = () => _servicio.ObtenerProveedorAsync(TipoProveedorTts.WebSpeechApi);

        // Assert
        await accion.Should().ThrowAsync<ExcepcionNoEncontrado>();
    }

    [Fact]
    public async Task ActualizarProveedorAsync_DebeActualizarCampos()
    {
        // Arrange
        PeticionGuardarProveedorTts peticion = new PeticionGuardarProveedorTts
        {
            NombreVisible = "Azure Actualizado",
            Habilitado = true,
            ApiKey = "mi-api-key-secreta",
            Region = "eastus",
            VozPorDefecto = "es-ES-AlvaroNeural"
        };

        // Act
        ConfiguracionProveedorTtsDto resultado = await _servicio.ActualizarProveedorAsync(
            TipoProveedorTts.AzureSpeech, peticion);

        // Assert
        resultado.NombreVisible.Should().Be("Azure Actualizado");
        resultado.Habilitado.Should().BeTrue();
        resultado.TieneApiKey.Should().BeTrue();
        resultado.Region.Should().Be("eastus");
        resultado.VozPorDefecto.Should().Be("es-ES-AlvaroNeural");
    }

    [Fact]
    public async Task ActualizarProveedorAsync_SinApiKey_NoDebeSobrescribir()
    {
        // Arrange — primero configurar una API key
        ProveedorTts? proveedor = await _contexto.ProveedoresTts
            .FirstAsync(p => p.Tipo == TipoProveedorTts.AzureSpeech);
        proveedor.ApiKey = "clave-existente";
        await _contexto.SaveChangesAsync();

        PeticionGuardarProveedorTts peticion = new PeticionGuardarProveedorTts
        {
            NombreVisible = "Nombre Nuevo",
            Habilitado = true
            // ApiKey no se envía — no debe borrarse
        };

        // Act
        ConfiguracionProveedorTtsDto resultado = await _servicio.ActualizarProveedorAsync(
            TipoProveedorTts.AzureSpeech, peticion);

        // Assert
        resultado.TieneApiKey.Should().BeTrue();
    }

    [Fact]
    public async Task AlternarProveedorAsync_DebeAlternarEstado()
    {
        // Act — deshabilitado → habilitado
        bool resultado1 = await _servicio.AlternarProveedorAsync(TipoProveedorTts.AzureSpeech);

        // Assert
        resultado1.Should().BeTrue();

        // Act — habilitado → deshabilitado
        bool resultado2 = await _servicio.AlternarProveedorAsync(TipoProveedorTts.AzureSpeech);

        // Assert
        resultado2.Should().BeFalse();
    }

    [Fact]
    public async Task AlternarProveedorAsync_ProveedorInexistente_DebeLanzarExcepcion()
    {
        // Act
        Func<Task> accion = () => _servicio.AlternarProveedorAsync(TipoProveedorTts.WebSpeechApi);

        // Assert
        await accion.Should().ThrowAsync<ExcepcionNoEncontrado>();
    }

    [Fact]
    public async Task ObtenerProveedoresAsync_DebeOrdenarPorOrden()
    {
        // Arrange — invertir el orden
        ProveedorTts? azure = await _contexto.ProveedoresTts.FirstAsync(p => p.Tipo == TipoProveedorTts.AzureSpeech);
        azure.Orden = 10;
        await _contexto.SaveChangesAsync();

        // Act
        IReadOnlyList<ConfiguracionProveedorTtsDto> resultado = await _servicio.ObtenerProveedoresAsync();

        // Assert
        resultado[0].Tipo.Should().Be("ElevenLabs"); // orden 2
        resultado[1].Tipo.Should().Be("AzureSpeech"); // orden 10
    }

    public void Dispose()
    {
        _contexto.Dispose();
        _conexionSqlite.Dispose();
    }
}
