using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace SkillUpAcademy.IntegrationTests.Controladores;

/// <summary>
/// Tests de integración para HealthController.
/// </summary>
[Trait("Category", "Integration")]
[Collection("Integration")]
public class HealthControllerTests
{
    private readonly CustomWebApplicationFactory _factory;

    public HealthControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Health_DebeRetornar200ConEstadoSaludable()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage respuesta = await client.GetAsync("/api/v1/health");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.OK);

        string json = await respuesta.Content.ReadAsStringAsync();
        using JsonDocument documento = JsonDocument.Parse(json);
        JsonElement raiz = documento.RootElement;

        raiz.GetProperty("estado").GetString().Should().Be("saludable");
        raiz.TryGetProperty("timestamp", out _).Should().BeTrue();
    }

    [Fact]
    public async Task Ready_ConInMemoryDb_DebeRetornar200()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage respuesta = await client.GetAsync("/api/v1/health/ready");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.OK);

        string json = await respuesta.Content.ReadAsStringAsync();
        using JsonDocument documento = JsonDocument.Parse(json);
        JsonElement raiz = documento.RootElement;

        raiz.GetProperty("estado").GetString().Should().Be("listo");
    }
}
