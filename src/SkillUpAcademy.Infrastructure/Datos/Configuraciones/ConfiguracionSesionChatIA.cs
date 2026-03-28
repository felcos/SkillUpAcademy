using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionSesionChatIA : IEntityTypeConfiguration<SesionChatIA>
{
    public void Configure(EntityTypeBuilder<SesionChatIA> builder)
    {
        builder.ToTable("sesiones_chat_ia");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(s => s.UsuarioId).HasColumnName("usuario_id");
        builder.Property(s => s.LeccionId).HasColumnName("leccion_id");
        builder.Property(s => s.TipoSesion).HasColumnName("tipo_sesion").HasConversion<string>().HasMaxLength(30);
        builder.Property(s => s.FechaInicio).HasColumnName("fecha_inicio").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
        builder.Property(s => s.FechaFin).HasColumnName("fecha_fin").HasColumnType("timestamptz");
        builder.Property(s => s.ContadorMensajes).HasColumnName("contador_mensajes").HasDefaultValue(0);
        builder.Property(s => s.FueMarcada).HasColumnName("fue_marcada").HasDefaultValue(false);
        builder.Property(s => s.Activa).HasColumnName("activa").HasDefaultValue(true);

        builder.HasIndex(s => s.UsuarioId);
        builder.HasOne(s => s.Usuario).WithMany(u => u.SesionesChatIA).HasForeignKey(s => s.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(s => s.Leccion).WithMany().HasForeignKey(s => s.LeccionId).OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(s => s.Mensajes).WithOne(m => m.Sesion).HasForeignKey(m => m.SesionId).OnDelete(DeleteBehavior.Cascade);
    }
}
