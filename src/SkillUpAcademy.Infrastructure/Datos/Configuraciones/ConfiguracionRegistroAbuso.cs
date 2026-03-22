using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionRegistroAbuso : IEntityTypeConfiguration<RegistroAbuso>
{
    public void Configure(EntityTypeBuilder<RegistroAbuso> builder)
    {
        builder.ToTable("registros_abuso");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.UsuarioId).HasColumnName("usuario_id");
        builder.Property(r => r.SesionId).HasColumnName("sesion_id");
        builder.Property(r => r.TipoViolacion).HasColumnName("tipo_violacion").HasConversion<string>().HasMaxLength(50);
        builder.Property(r => r.MensajeOriginal).HasColumnName("mensaje_original").HasColumnType("text");
        builder.Property(r => r.MetodoDeteccion).HasColumnName("metodo_deteccion").HasMaxLength(50);
        builder.Property(r => r.AccionTomada).HasColumnName("accion_tomada").HasConversion<string>().HasMaxLength(50);
        builder.Property(r => r.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasIndex(r => r.UsuarioId);
        builder.HasOne(r => r.Usuario).WithMany().HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.Sesion).WithMany().HasForeignKey(r => r.SesionId).OnDelete(DeleteBehavior.SetNull);
    }
}
