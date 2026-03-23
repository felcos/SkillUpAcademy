using System.Net;
using FluentAssertions;

namespace SkillUpAcademy.IntegrationTests.Controladores;

/// <summary>
/// Tests de integración que verifican que los endpoints protegidos rechazan peticiones sin autenticación.
/// </summary>
[Trait("Category", "Integration")]
[Collection("Integration")]
public class EndpointsProtegidosTests
{
    private readonly CustomWebApplicationFactory _factory;

    public EndpointsProtegidosTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("GET", "/api/v1/lessons/1")]
    [InlineData("GET", "/api/v1/lessons/1/scenes")]
    [InlineData("POST", "/api/v1/lessons/1/start")]
    [InlineData("POST", "/api/v1/lessons/1/complete")]
    [InlineData("GET", "/api/v1/lessons/1/quiz/")]
    [InlineData("GET", "/api/v1/lessons/1/scenario/")]
    [InlineData("GET", "/api/v1/progress/dashboard")]
    [InlineData("GET", "/api/v1/progress/achievements")]
    [InlineData("GET", "/api/v1/auth/me")]
    public async Task EndpointsProtegidos_SinToken_DebenRetornar401(string metodo, string ruta)
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage respuesta = metodo switch
        {
            "GET" => await client.GetAsync(ruta),
            "POST" => await client.PostAsync(ruta, null),
            _ => throw new ArgumentException($"Método HTTP no soportado: {metodo}")
        };

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
