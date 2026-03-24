using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionUsuarioApp : IEntityTypeConfiguration<UsuarioApp>
{
    public void Configure(EntityTypeBuilder<UsuarioApp> builder)
    {
        builder.Property(u => u.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
        builder.Property(u => u.Apellidos).HasColumnName("apellidos").HasMaxLength(100).IsRequired();
        builder.Property(u => u.UrlAvatar).HasColumnName("url_avatar").HasMaxLength(500);
        builder.Property(u => u.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
        builder.Property(u => u.UltimoAcceso).HasColumnName("ultimo_acceso").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
        builder.Property(u => u.PuntosTotales).HasColumnName("puntos_totales").HasDefaultValue(0);
        builder.Property(u => u.IdiomaPreferido).HasColumnName("idioma_preferido").HasMaxLength(5).HasDefaultValue("es");
        builder.Property(u => u.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(u => u.AudioHabilitado).HasColumnName("audio_habilitado").HasDefaultValue(true);
        builder.Property(u => u.RachaDias).HasColumnName("racha_dias").HasDefaultValue(0);
        builder.Property(u => u.UltimaFechaActividad).HasColumnName("ultima_fecha_actividad").HasColumnType("timestamptz");
        builder.Property(u => u.VozPreferida).HasColumnName("voz_preferida").HasMaxLength(100);
        builder.Property(u => u.VelocidadVoz).HasColumnName("velocidad_voz").HasDefaultValue(1.0m);
        builder.Property(u => u.ProveedorTtsPreferido).HasColumnName("proveedor_tts_preferido").HasMaxLength(30).HasDefaultValue("WebSpeechApi");
    }
}
