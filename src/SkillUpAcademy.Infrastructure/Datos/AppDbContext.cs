using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Contexto de base de datos principal de SkillUp Academy.
/// </summary>
public class AppDbContext : IdentityDbContext<UsuarioApp, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> opciones) : base(opciones)
    {
    }

    // Contenido educativo
    public DbSet<AreaHabilidad> AreasHabilidad => Set<AreaHabilidad>();
    public DbSet<Nivel> Niveles => Set<Nivel>();
    public DbSet<Leccion> Lecciones => Set<Leccion>();
    public DbSet<PreguntaQuiz> PreguntasQuiz => Set<PreguntaQuiz>();
    public DbSet<OpcionQuiz> OpcionesQuiz => Set<OpcionQuiz>();
    public DbSet<Escenario> Escenarios => Set<Escenario>();
    public DbSet<OpcionEscenario> OpcionesEscenario => Set<OpcionEscenario>();

    // Progreso del usuario
    public DbSet<ProgresoUsuario> ProgresosUsuario => Set<ProgresoUsuario>();
    public DbSet<RespuestaQuizUsuario> RespuestasQuizUsuario => Set<RespuestaQuizUsuario>();
    public DbSet<EleccionEscenarioUsuario> EleccionesEscenarioUsuario => Set<EleccionEscenarioUsuario>();

    // Chat IA
    public DbSet<SesionChatIA> SesionesChatIA => Set<SesionChatIA>();
    public DbSet<MensajeChatIA> MensajesChatIA => Set<MensajeChatIA>();

    // Gamificación
    public DbSet<Logro> Logros => Set<Logro>();
    public DbSet<LogroUsuario> LogrosUsuario => Set<LogroUsuario>();

    // Seguridad
    public DbSet<RegistroAbuso> RegistrosAbuso => Set<RegistroAbuso>();

    // Motor de escenas (avatar)
    public DbSet<EscenaLeccion> EscenasLeccion => Set<EscenaLeccion>();
    public DbSet<RecursoVisual> RecursosVisuales => Set<RecursoVisual>();
    public DbSet<ConfiguracionAvatar> ConfiguracionesAvatar => Set<ConfiguracionAvatar>();

    // TTS (proveedores de voz)
    public DbSet<ProveedorTts> ProveedoresTts => Set<ProveedorTts>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar todas las configuraciones del assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Renombrar tablas de Identity a snake_case para PostgreSQL
        modelBuilder.Entity<UsuarioApp>().ToTable("usuarios");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("roles");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("usuarios_roles");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("usuarios_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("usuarios_logins");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("usuarios_tokens");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("roles_claims");
    }
}
