using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionConfiguracionAvatar : IEntityTypeConfiguration<ConfiguracionAvatar>
{
    public void Configure(EntityTypeBuilder<ConfiguracionAvatar> builder)
    {
        builder.ToTable("configuraciones_avatar");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.NombreAvatar).HasColumnName("nombre_avatar").HasMaxLength(50).HasDefaultValue("Aria");
        builder.Property(c => c.Slug).HasColumnName("slug").HasMaxLength(50).IsRequired();
        builder.HasIndex(c => c.Slug).IsUnique();
        builder.Property(c => c.DescripcionPersonalidad).HasColumnName("descripcion_personalidad").HasColumnType("text");
        builder.Property(c => c.Tono).HasColumnName("tono").HasMaxLength(30).HasDefaultValue("profesional");
        builder.Property(c => c.VelocidadHabla).HasColumnName("velocidad_habla").HasDefaultValue(1.0m);
        builder.Property(c => c.VozTTS).HasColumnName("voz_tts").HasMaxLength(100).HasDefaultValue("es-ES-ElviraNeural");
        builder.Property(c => c.PromptSistemaBase).HasColumnName("prompt_sistema_base").HasColumnType("text");
        builder.Property(c => c.EstiloVisual).HasColumnName("estilo_visual").HasMaxLength(50).HasDefaultValue("corporativa");
        builder.Property(c => c.ColorFondo).HasColumnName("color_fondo").HasMaxLength(7);
        builder.Property(c => c.EstiloCelebracion).HasColumnName("estilo_celebracion").HasMaxLength(30).HasDefaultValue("moderado");
        builder.Property(c => c.EstiloCorreccion).HasColumnName("estilo_correccion").HasMaxLength(30).HasDefaultValue("constructivo");
        builder.Property(c => c.EsDefault).HasColumnName("es_default").HasDefaultValue(false);
        builder.Property(c => c.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(c => c.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
    }
}
