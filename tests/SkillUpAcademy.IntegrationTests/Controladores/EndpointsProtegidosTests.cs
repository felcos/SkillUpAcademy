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
    [InlineData("POST", "/api/v1/ai/session/00000000-0000-0000-0000-000000000001/message")]
    [InlineData("POST", "/api/v1/ai/session/00000000-0000-0000-0000-000000000001/message/stream")]
    [InlineData("GET", "/api/v1/admin/resumen")]
    [InlineData("GET", "/api/v1/admin/usuarios")]
    [InlineData("GET", "/api/v1/admin/estadisticas-contenido")]
    [InlineData("POST", "/api/v1/admin/usuarios/00000000-0000-0000-0000-000000000001/alternar-bloqueo-ia")]
    [InlineData("GET", "/api/v1/tts/voces")]
    [InlineData("GET", "/api/v1/tts/configuracion")]
    [InlineData("PUT", "/api/v1/tts/preferencias")]
    [InlineData("POST", "/api/v1/tts/sintetizar")]
    [InlineData("GET", "/api/v1/admin/tts/proveedores")]
    [InlineData("POST", "/api/v1/admin/tts/proveedores/AzureSpeech/alternar")]
    public async Task EndpointsProtegidos_SinToken_DebenRetornar401(string metodo, string ruta)
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage respuesta = metodo switch
        {
            "GET" => await client.GetAsync(ruta),
            "POST" => await client.PostAsync(ruta, null),
            "PUT" => await client.PutAsync(ruta, null),
            _ => throw new ArgumentException($"Método HTTP no soportado: {metodo}")
        };

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
