using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionAreaHabilidad : IEntityTypeConfiguration<AreaHabilidad>
{
    public void Configure(EntityTypeBuilder<AreaHabilidad> builder)
    {
        builder.ToTable("areas_habilidad");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Slug).HasColumnName("slug").HasMaxLength(50).IsRequired();
        builder.HasIndex(a => a.Slug).IsUnique();
        builder.Property(a => a.Titulo).HasColumnName("titulo").HasMaxLength(100).IsRequired();
        builder.Property(a => a.Subtitulo).HasColumnName("subtitulo").HasMaxLength(200);
        builder.Property(a => a.Descripcion).HasColumnName("descripcion").HasColumnType("text");
        builder.Property(a => a.Icono).HasColumnName("icono").HasMaxLength(10);
        builder.Property(a => a.ColorPrimario).HasColumnName("color_primario").HasMaxLength(7);
        builder.Property(a => a.ColorAcento).HasColumnName("color_acento").HasMaxLength(7);
        builder.Property(a => a.Orden).HasColumnName("orden").HasDefaultValue(0);
        builder.Property(a => a.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(a => a.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasMany(a => a.Niveles).WithOne(n => n.AreaHabilidad).HasForeignKey(n => n.AreaHabilidadId).OnDelete(DeleteBehavior.Cascade);
    }
}
