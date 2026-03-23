using System.Net;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.IntegrationTests.Controladores;

/// <summary>
/// Tests de integración para SkillsController.
/// </summary>
[Trait("Category", "Integration")]
[Collection("Integration")]
public class SkillsControllerTests
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public SkillsControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerAreas_SinAutenticacion_DebeRetornar200()
    {
        // Act
        HttpResponseMessage respuesta = await _client.GetAsync("/api/v1/skills");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ObtenerAreas_ConDatosSembrados_DebeRetornarLista()
    {
        // Arrange — sembrar un área
        using IServiceScope scope = _factory.Services.CreateScope();
        AppDbContext contexto = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!contexto.Set<AreaHabilidad>().Any())
        {
            contexto.Set<AreaHabilidad>().Add(new AreaHabilidad
            {
                Titulo = "Comunicación Efectiva",
                Slug = "comunicacion-efectiva",
                Descripcion = "Aprende a comunicar ideas",
                Icono = "💬",
                ColorPrimario = "#3498DB",
                Orden = 1,
                Niveles = new List<Nivel>
                {
                    new Nivel
                    {
                        NumeroNivel = 1,
                        Nombre = "Fundamentos",
                        Descripcion = "Bases de comunicación",
                        PuntosDesbloqueo = 0,
                        Lecciones = new List<Leccion>
                        {
                            new Leccion
                            {
                                Titulo = "Introducción a la comunicación",
                                TipoLeccion = TipoLeccion.Teoria,
                                Contenido = "Contenido de prueba",
                                PuntosRecompensa = 10,
                                Orden = 1,
                                DuracionMinutos = 5
                            }
                        }
                    }
                }
            });
            await contexto.SaveChangesAsync();
        }

        // Act
        HttpResponseMessage respuesta = await _client.GetAsync("/api/v1/skills");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.OK);

        string json = await respuesta.Content.ReadAsStringAsync();
        using JsonDocument documento = JsonDocument.Parse(json);
        JsonElement raiz = documento.RootElement;

        raiz.GetArrayLength().Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ObtenerArea_SlugInexistente_DebeRetornar404()
    {
        // Act
        HttpResponseMessage respuesta = await _client.GetAsync("/api/v1/skills/slug-inexistente-xyz");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ObtenerNiveles_SinAutenticacion_DebeRetornar401()
    {
        // Act
        HttpResponseMessage respuesta = await _client.GetAsync("/api/v1/skills/comunicacion-efectiva/levels");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ObtenerNivel_SinAutenticacion_DebeRetornar401()
    {
        // Act
        HttpResponseMessage respuesta = await _client.GetAsync("/api/v1/skills/comunicacion-efectiva/levels/1");

        // Assert
        respuesta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
