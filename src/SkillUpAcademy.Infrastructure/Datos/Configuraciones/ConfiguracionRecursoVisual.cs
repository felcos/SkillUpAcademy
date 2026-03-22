using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionRecursoVisual : IEntityTypeConfiguration<RecursoVisual>
{
    public void Configure(EntityTypeBuilder<RecursoVisual> builder)
    {
        builder.ToTable("recursos_visuales");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.EscenaLeccionId).HasColumnName("escena_leccion_id");
        builder.Property(r => r.TipoRecurso).HasColumnName("tipo_recurso").HasConversion<string>().HasMaxLength(30);
        builder.Property(r => r.Nombre).HasColumnName("nombre").HasMaxLength(200).IsRequired();
        builder.Property(r => r.Url).HasColumnName("url").HasMaxLength(1000).IsRequired();
        builder.Property(r => r.TextoAlternativo).HasColumnName("texto_alternativo").HasMaxLength(500);
        builder.Property(r => r.Etiquetas).HasColumnName("etiquetas").HasColumnType("jsonb");
        builder.Property(r => r.TamanoBytes).HasColumnName("tamano_bytes");
        builder.Property(r => r.TipoMime).HasColumnName("tipo_mime").HasMaxLength(100);
        builder.Property(r => r.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
    }
}
