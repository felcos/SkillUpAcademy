using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.IntegrationTests;

/// <summary>
/// Factory personalizada que reemplaza PostgreSQL por InMemory para tests de integración.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "IntegrationTestDb_" + Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        // Inyectar configuración que necesita el Program.cs para no fallar
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = "Host=localhost;Database=test;"
            });
        });

        builder.ConfigureServices(services =>
        {
            // Remover el DbContext registrado con PostgreSQL (Npgsql)
            ServiceDescriptor? descriptorDb = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptorDb != null)
            {
                services.Remove(descriptorDb);
            }

            // Registrar InMemory en su lugar
            services.AddDbContext<AppDbContext>(opciones =>
            {
                opciones.UseInMemoryDatabase(_dbName);
            });
        });
    }
}
