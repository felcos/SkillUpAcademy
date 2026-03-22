using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SkillUpAcademy.Api.Extensiones;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Infrastructure.Datos;
using System.Text;

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

    // JWT
    string jwtSecret = builder.Configuration["Jwt:Secret"] ?? "CAMBIAR_ESTA_CLAVE_SECRETA_POR_UNA_REAL_DE_32_CARACTERES_MINIMO";
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

    // CORS
    builder.Services.AddCors(opciones =>
    {
        opciones.AddPolicy("PermitirFrontend", politica =>
            politica.WithOrigins("http://localhost:5173", "http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
    });

    WebApplication app = builder.Build();

    // Pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<SkillUpAcademy.Api.Middleware.MiddlewareCabecerasSeguridad>();
    app.UseMiddleware<SkillUpAcademy.Api.Middleware.MiddlewareManejoExcepciones>();

    app.UseSerilogRequestLogging();
    app.UseCors("PermitirFrontend");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

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
