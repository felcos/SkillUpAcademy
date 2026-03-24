using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Repositorios;
using SkillUpAcademy.Infrastructure.Servicios;

namespace SkillUpAcademy.Api.Extensiones;

/// <summary>
/// Extensiones para registrar servicios en el contenedor de DI.
/// </summary>
public static class ExtensionesDeServicios
{
    /// <summary>
    /// Registra todos los repositorios y servicios de la aplicación.
    /// </summary>
    public static IServiceCollection AgregarServiciosDeAplicacion(this IServiceCollection servicios)
    {
        // Repositorios
        servicios.AddScoped<IRepositorioAreasHabilidad, RepositorioAreasHabilidad>();
        servicios.AddScoped<IRepositorioLecciones, RepositorioLecciones>();
        servicios.AddScoped<IRepositorioProgreso, RepositorioProgreso>();
        servicios.AddScoped<IRepositorioChatIA, RepositorioChatIA>();
        servicios.AddScoped<IRepositorioLogros, RepositorioLogros>();

        // Servicios
        servicios.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
        servicios.AddScoped<IServicioHabilidades, ServicioHabilidades>();
        servicios.AddScoped<IServicioLecciones, ServicioLecciones>();
        servicios.AddScoped<IServicioQuiz, ServicioQuiz>();
        servicios.AddScoped<IServicioEscenario, ServicioEscenario>();
        servicios.AddScoped<IServicioProgreso, ServicioProgreso>();
        servicios.AddScoped<IServicioAdmin, ServicioAdmin>();
        servicios.AddScoped<IServicioEscenas, ServicioEscenas>();
        servicios.AddScoped<IServicioSeguridadIA, ServicioSeguridadIA>();

        // Servicios admin especializados
        servicios.AddScoped<IServicioAdminTts, ServicioAdminTts>();

        // Servicios con HttpClient
        servicios.AddHttpClient<IServicioChatIA, ServicioChatIA>();
        servicios.AddHttpClient<IServicioTts, ServicioTts>();

        // Cache en memoria
        servicios.AddMemoryCache();

        return servicios;
    }
}
