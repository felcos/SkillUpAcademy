using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionProgresoUsuario : IEntityTypeConfiguration<ProgresoUsuario>
{
    public void Configure(EntityTypeBuilder<ProgresoUsuario> builder)
    {
        builder.ToTable("progreso_usuario");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.UsuarioId).HasColumnName("usuario_id");
        builder.Property(p => p.LeccionId).HasColumnName("leccion_id");
        builder.Property(p => p.Estado).HasColumnName("estado").HasConversion<string>().HasMaxLength(20).HasDefaultValue(Core.Enums.EstadoProgreso.NoIniciado);
        builder.Property(p => p.Puntuacion).HasColumnName("puntuacion").HasDefaultValue(0);
        builder.Property(p => p.Intentos).HasColumnName("intentos").HasDefaultValue(0);
        builder.Property(p => p.FechaCompletado).HasColumnName("fecha_completado").HasColumnType("timestamptz");
        builder.Property(p => p.UltimoAcceso).HasColumnName("ultimo_acceso").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasIndex(p => new { p.UsuarioId, p.LeccionId }).IsUnique();
        builder.HasIndex(p => p.UsuarioId);
        builder.HasIndex(p => p.LeccionId);

        builder.HasOne(p => p.Usuario).WithMany(u => u.Progresos).HasForeignKey(p => p.UsuarioId).OnDelete(DeleteBehavior.Cascade);
    }
}
