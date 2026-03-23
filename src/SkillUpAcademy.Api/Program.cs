using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SkillUpAcademy.Api.Extensiones;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Infrastructure.Datos;
using System.Text;

// Alias para RoleManager usado en el sembrado de admin
using RoleManagerType = Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>>;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Serilog
    builder.Host.UseSerilog((contexto, servicios, configuracion) => configuracion
        .ReadFrom.Configuration(contexto.Configuration)
        .ReadFrom.Services(servicios)
        .WriteTo.Console());

    // PostgreSQL + EF Core
    string? cadenaConexion = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(opciones =>
        opciones.UseNpgsql(cadenaConexion));

    // ASP.NET Identity
    builder.Services.AddIdentity<UsuarioApp, IdentityRole<Guid>>(opciones =>
    {
        opciones.Password.RequireDigit = true;
        opciones.Password.RequireLowercase = true;
        opciones.Password.RequireUppercase = true;
        opciones.Password.RequireNonAlphanumeric = true;
        opciones.Password.RequiredLength = 8;
        opciones.Lockout.MaxFailedAccessAttempts = 5;
        opciones.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        opciones.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

    // JWT — Validar que el secret no sea el placeholder en producción
    string jwtSecret = builder.Configuration["Jwt:Secret"] ?? "CAMBIAR_ESTA_CLAVE_SECRETA_POR_UNA_REAL_DE_32_CARACTERES_MINIMO";
    if (!builder.Environment.IsDevelopment()
        && !builder.Environment.IsEnvironment("Testing")
        && jwtSecret.StartsWith("CAMBIAR_ESTA_CLAVE"))
    {
        throw new InvalidOperationException(
            "El Jwt:Secret no puede ser el placeholder en producción. " +
            "Configure la variable de entorno Jwt__Secret con una clave segura de al menos 64 caracteres.");
    }
    builder.Services.AddAuthentication(opciones =>
    {
        opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });
    builder.Services.AddAuthorization();

    // Controllers + Swagger
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Servicios de la aplicación (repositorios + servicios)
    builder.Services.AgregarServiciosDeAplicacion();

    // CORS — Orígenes desde configuración (producción) o localhost (desarrollo)
    string[] origenesPermitidos = builder.Environment.IsDevelopment()
        ? new[] { "http://localhost:5173", "http://localhost:3000" }
        : builder.Configuration.GetSection("Cors:OrigenesPermitidos").Get<string[]>() ?? Array.Empty<string>();

    builder.Services.AddCors(opciones =>
    {
        opciones.AddPolicy("PermitirFrontend", politica =>
            politica.WithOrigins(origenesPermitidos)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
    });

    WebApplication app = builder.Build();

    // Aplicar migraciones y sembrar datos en desarrollo
    if (app.Environment.IsDevelopment())
    {
        using IServiceScope scope = app.Services.CreateScope();
        AppDbContext contexto = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await contexto.Database.MigrateAsync();
        await SembradoDatos.SembrarAsync(contexto);

        // Sembrar rol Admin y usuario administrador
        RoleManagerType roleManager = scope.ServiceProvider.GetRequiredService<RoleManagerType>();
        UserManager<UsuarioApp> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UsuarioApp>>();
        await SembradoAdmin.SembrarAsync(roleManager, userManager);
    }

    // Pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseHsts();
        app.UseHttpsRedirection();
    }

    app.UseMiddleware<SkillUpAcademy.Api.Middleware.MiddlewareCabecerasSeguridad>();
    app.UseMiddleware<SkillUpAcademy.Api.Middleware.MiddlewareManejoExcepciones>();

    app.UseSerilogRequestLogging();
    app.UseCors("PermitirFrontend");
    app.UseAuthentication();
    app.UseAuthorization();

    // Servir archivos estáticos del SPA (wwwroot)
    app.UseStaticFiles();

    app.MapControllers();

    // Fallback: en producción, cualquier ruta que no sea /api/* devuelve index.html
    // para que el router del SPA (React Router) maneje la navegación client-side
    if (!app.Environment.IsDevelopment())
    {
        app.MapFallbackToFile("index.html");
    }

    Log.Information("SkillUp Academy iniciado correctamente");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}

/// <summary>
/// Clase parcial necesaria para que WebApplicationFactory pueda descubrir el punto de entrada.
/// </summary>
public partial class Program { }
