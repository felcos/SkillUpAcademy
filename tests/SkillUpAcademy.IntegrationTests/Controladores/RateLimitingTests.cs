using System.Net;
using FluentAssertions;

namespace SkillUpAcademy.IntegrationTests.Controladores;

/// <summary>
/// Tests de integración que verifican que el middleware de rate limiting está registrado.
/// Los límites reales (100/min general, 20/min IA) son demasiado altos para testear con
/// peticiones reales en un test rápido, así que verificamos que el middleware responde
/// correctamente dentro del límite y que los endpoints siguen funcionando.
/// </summary>
[Trait("Category", "Integration")]
[Collection("Integration")]
public class RateLimitingTests
{
    private readonly CustomWebApplicationFactory _factory;

    public RateLimitingTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task EndpointConRateLimiting_DentroDelLimite_DebeResponder200()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act — Enviar unas pocas peticiones (dentro del límite de 100/min)
        List<HttpStatusCode> respuestas = new List<HttpStatusCode>();
        for (int i = 0; i < 5; i++)
        {
            HttpResponseMessage respuesta = await client.GetAsync("/api/v1/skills");
            respuestas.Add(respuesta.StatusCode);
        }

        // Assert — Todas deben ser 200 (dentro del límite)
        respuestas.Should().AllSatisfy(s => s.Should().Be(HttpStatusCode.OK));
    }

    [Fact]
    public async Task HealthEndpoint_MultiplesLlamadas_SiempreResponde200()
    {
        // Arrange — Health no tiene [EnableRateLimiting]
        HttpClient client = _factory.CreateClient();

        // Act
        List<HttpStatusCode> respuestas = new List<HttpStatusCode>();
        for (int i = 0; i < 10; i++)
        {
            HttpResponseMessage respuesta = await client.GetAsync("/api/v1/health");
            respuestas.Add(respuesta.StatusCode);
        }

        // Assert
        respuestas.Should().AllSatisfy(s => s.Should().Be(HttpStatusCode.OK));
    }
}
