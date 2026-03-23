using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;

namespace SkillUpAcademy.IntegrationTests.Controladores;

/// <summary>
/// Tests de integración para AuthController.
/// </summary>
[Trait("Category", "Integration")]
[Collection("Integration")]
public class AuthControllerTests
{
    private readonly CustomWebApplicationFactory _factory;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Registro_DatosValidos_DebeRetornar201ConToken()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        object datosRegistro = new
        {
            nombre = "Test",
            apellidos = "Usuario",
            email = $"test-{Guid.NewGuid():N}@test.com",
            contrasena = "Test@1234"
        };

        StringContent contenido = new StringContent(
            JsonSerializer.Serialize(datosRegistro),
            Encoding.UTF8,
            "application/json");

        // Act
        HttpResponseMessage respuesta = await client.PostAsync("/api/v1/auth/register", contenido);

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.Created);

        string json = await respuesta.Content.ReadAsStringAsync();
        using JsonDocument documento = JsonDocument.Parse(json);
        JsonElement raiz = documento.RootElement;

        raiz.TryGetProperty("tokenAcceso", out JsonElement token).Should().BeTrue();
        token.GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Registro_ContrasenDebil_DebeRetornar400()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        object datosRegistro = new
        {
            nombre = "Test",
            apellidos = "Usuario",
            email = $"test-{Guid.NewGuid():N}@test.com",
            contrasena = "123"
        };

        StringContent contenido = new StringContent(
            JsonSerializer.Serialize(datosRegistro),
            Encoding.UTF8,
            "application/json");

        // Act
        HttpResponseMessage respuesta = await client.PostAsync("/api/v1/auth/register", contenido);

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Login_CredencialesInvalidas_DebeRetornar400()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        object datosLogin = new
        {
            email = "noexiste@test.com",
            contrasena = "CualquierCosa@123"
        };

        StringContent contenido = new StringContent(
            JsonSerializer.Serialize(datosLogin),
            Encoding.UTF8,
            "application/json");

        // Act
        HttpResponseMessage respuesta = await client.PostAsync("/api/v1/auth/login", contenido);

        // Assert
        respuesta.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RegistroYLogin_FlowCompleto_DebeRetornarTokenValido()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        string email = $"flow-{Guid.NewGuid():N}@test.com";
        string contrasena = "Flow@1234";

        // Registro
        object datosRegistro = new { nombre = "Flow", apellidos = "Test", email, contrasena };
        StringContent contenidoRegistro = new StringContent(
            JsonSerializer.Serialize(datosRegistro), Encoding.UTF8, "application/json");

        HttpResponseMessage respuestaRegistro = await client.PostAsync("/api/v1/auth/register", contenidoRegistro);
        respuestaRegistro.StatusCode.Should().Be(HttpStatusCode.Created);

        // Login
        object datosLogin = new { email, contrasena };
        StringContent contenidoLogin = new StringContent(
            JsonSerializer.Serialize(datosLogin), Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage respuestaLogin = await client.PostAsync("/api/v1/auth/login", contenidoLogin);

        // Assert
        respuestaLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        string json = await respuestaLogin.Content.ReadAsStringAsync();
        using JsonDocument documento = JsonDocument.Parse(json);
        JsonElement raiz = documento.RootElement;

        raiz.GetProperty("tokenAcceso").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Me_SinToken_DebeRetornar401()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage respuesta = await client.GetAsync("/api/v1/auth/me");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Me_ConTokenValido_DebeRetornarPerfil()
    {
        // Arrange — registrar y obtener token
        HttpClient client = _factory.CreateClient();
        string email = $"me-{Guid.NewGuid():N}@test.com";
        object datosRegistro = new
        {
            nombre = "Perfil",
            apellidos = "Test",
            email,
            contrasena = "Perfil@1234"
        };

        StringContent contenidoRegistro = new StringContent(
            JsonSerializer.Serialize(datosRegistro), Encoding.UTF8, "application/json");

        HttpResponseMessage respuestaRegistro = await client.PostAsync("/api/v1/auth/register", contenidoRegistro);
        respuestaRegistro.StatusCode.Should().Be(HttpStatusCode.Created);

        string jsonRegistro = await respuestaRegistro.Content.ReadAsStringAsync();
        using JsonDocument docRegistro = JsonDocument.Parse(jsonRegistro);
        string token = docRegistro.RootElement.GetProperty("tokenAcceso").GetString()!;

        // Act
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage respuesta = await client.GetAsync("/api/v1/auth/me");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.OK);

        string json = await respuesta.Content.ReadAsStringAsync();
        using JsonDocument documento = JsonDocument.Parse(json);
        JsonElement raiz = documento.RootElement;

        raiz.GetProperty("nombre").GetString().Should().Be("Perfil");
        raiz.GetProperty("email").GetString().Should().Be(email);
    }
}
