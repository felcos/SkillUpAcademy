using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

/// <summary>
/// Configuración Fluent API para PlanAccionUsuario.
/// </summary>
public class ConfiguracionPlanAccionUsuario : IEntityTypeConfiguration<PlanAccionUsuario>
{
    public void Configure(EntityTypeBuilder<PlanAccionUsuario> builder)
    {
        builder.ToTable("planes_accion_usuario");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.UsuarioId).HasColumnName("usuario_id");
        builder.Property(p => p.LeccionId).HasColumnName("leccion_id");
        builder.Property(p => p.Compromiso).HasColumnName("compromiso").HasColumnType("text").IsRequired();
        builder.Property(p => p.ContextoAplicacion).HasColumnName("contexto_aplicacion").HasColumnType("text").IsRequired();
        builder.Property(p => p.FechaObjetivo).HasColumnName("fecha_objetivo").HasColumnType("date");
        builder.Property(p => p.Completado).HasColumnName("completado").HasDefaultValue(false);
        builder.Property(p => p.ReflexionResultado).HasColumnName("reflexion_resultado").HasColumnType("text");
        builder.Property(p => p.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
        builder.Property(p => p.FechaCompletado).HasColumnName("fecha_completado").HasColumnType("timestamptz");

        builder.HasIndex(p => p.UsuarioId);
        builder.HasIndex(p => new { p.UsuarioId, p.LeccionId });

        builder.HasOne(p => p.Usuario).WithMany(u => u.PlanesAccion).HasForeignKey(p => p.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(p => p.Leccion).WithMany(l => l.PlanesAccion).HasForeignKey(p => p.LeccionId).OnDelete(DeleteBehavior.Cascade);
    }
}
