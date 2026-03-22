using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionMensajeChatIA : IEntityTypeConfiguration<MensajeChatIA>
{
    public void Configure(EntityTypeBuilder<MensajeChatIA> builder)
    {
        builder.ToTable("mensajes_chat_ia");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.SesionId).HasColumnName("sesion_id");
        builder.Property(m => m.Rol).HasColumnName("rol").HasMaxLength(20).IsRequired();
        builder.Property(m => m.Contenido).HasColumnName("contenido").HasColumnType("text").IsRequired();
        builder.Property(m => m.FueMarcado).HasColumnName("fue_marcado").HasDefaultValue(false);
        builder.Property(m => m.MotivoMarca).HasColumnName("motivo_marca").HasMaxLength(200);
        builder.Property(m => m.TokensUsados).HasColumnName("tokens_usados").HasDefaultValue(0);
        builder.Property(m => m.FechaEnvio).HasColumnName("fecha_envio").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasIndex(m => m.SesionId);
    }
}
