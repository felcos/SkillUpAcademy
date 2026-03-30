using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using SkillUpAcademy.Api.Hubs;
using SkillUpAcademy.Api.Servicios;
using SkillUpAcademy.Core.DTOs.Progreso;

namespace SkillUpAcademy.UnitTests.Servicios;

/// <summary>
/// Tests unitarios para ServicioNotificaciones.
/// </summary>
public class ServicioNotificacionesTests
{
    private readonly Mock<IHubContext<NotificacionesHub>> _mockHubContext;
    private readonly Mock<IHubClients> _mockClients;
    private readonly Mock<IClientProxy> _mockClientProxy;
    private readonly ServicioNotificaciones _servicio;

    public ServicioNotificacionesTests()
    {
        _mockHubContext = new Mock<IHubContext<NotificacionesHub>>();
        _mockClients = new Mock<IHubClients>();
        _mockClientProxy = new Mock<IClientProxy>();
        Mock<ILogger<ServicioNotificaciones>> mockLogger = new Mock<ILogger<ServicioNotificaciones>>();

        _mockHubContext.Setup(h => h.Clients).Returns(_mockClients.Object);
        _mockClients.Setup(c => c.User(It.IsAny<string>())).Returns(_mockClientProxy.Object);

        _servicio = new ServicioNotificaciones(_mockHubContext.Object, mockLogger.Object);
    }

    [Fact]
    public async Task NotificarLogroDesbloqueadoAsync_DebeEnviarAlUsuario()
    {
        // Arrange
        Guid usuarioId = Guid.NewGuid();
        LogroDto logro = new LogroDto
        {
            Id = 1,
            Slug = "first_lesson",
            Titulo = "Primera lección",
            Icono = "🎓",
            Descripcion = "Completaste tu primera lección",
            Desbloqueado = true,
            FechaDesbloqueo = DateTime.UtcNow
        };

        // Act
        await _servicio.NotificarLogroDesbloqueadoAsync(usuarioId, logro);

        // Assert
        _mockClients.Verify(c => c.User(usuarioId.ToString()), Times.Once);
        _mockClientProxy.Verify(p => p.SendCoreAsync(
            "LogroDesbloqueado",
            It.Is<object?[]>(args => args.Length == 1),
            default), Times.Once);
    }

    [Fact]
    public async Task NotificarLeccionCompletadaAsync_DebeEnviarAlUsuario()
    {
        // Arrange
        Guid usuarioId = Guid.NewGuid();

        // Act
        await _servicio.NotificarLeccionCompletadaAsync(usuarioId, "Escucha activa", 25);

        // Assert
        _mockClients.Verify(c => c.User(usuarioId.ToString()), Times.Once);
        _mockClientProxy.Verify(p => p.SendCoreAsync(
            "LeccionCompletada",
            It.Is<object?[]>(args => args.Length == 1),
            default), Times.Once);
    }

    [Fact]
    public async Task NotificarRachaActualizadaAsync_DebeEnviarAlUsuario()
    {
        // Arrange
        Guid usuarioId = Guid.NewGuid();

        // Act
        await _servicio.NotificarRachaActualizadaAsync(usuarioId, 5);

        // Assert
        _mockClients.Verify(c => c.User(usuarioId.ToString()), Times.Once);
        _mockClientProxy.Verify(p => p.SendCoreAsync(
            "RachaActualizada",
            It.Is<object?[]>(args => args.Length == 1),
            default), Times.Once);
    }

    [Fact]
    public async Task NotificarLogroDesbloqueadoAsync_NoLanzaExcepcion_SiHubFalla()
    {
        // Arrange
        Guid usuarioId = Guid.NewGuid();
        LogroDto logro = new LogroDto { Titulo = "Test", Icono = "🏆" };

        _mockClientProxy.Setup(p => p.SendCoreAsync(
            It.IsAny<string>(),
            It.IsAny<object?[]>(),
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Conexión perdida"));

        // Act
        Func<Task> accion = () => _servicio.NotificarLogroDesbloqueadoAsync(usuarioId, logro);

        // Assert — no debe lanzar excepción
        await accion.Should().NotThrowAsync();
    }
}
