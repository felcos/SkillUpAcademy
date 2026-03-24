using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionProveedorTts : IEntityTypeConfiguration<ProveedorTts>
{
    public void Configure(EntityTypeBuilder<ProveedorTts> builder)
    {
        builder.ToTable("proveedores_tts");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Tipo).HasColumnName("tipo").HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.HasIndex(p => p.Tipo).IsUnique();
        builder.Property(p => p.NombreVisible).HasColumnName("nombre_visible").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descripcion).HasColumnName("descripcion").HasColumnType("text");
        builder.Property(p => p.Habilitado).HasColumnName("habilitado").HasDefaultValue(false);
        builder.Property(p => p.ApiKey).HasColumnName("api_key").HasMaxLength(500);
        builder.Property(p => p.Region).HasColumnName("region").HasMaxLength(50);
        builder.Property(p => p.VozPorDefecto).HasColumnName("voz_por_defecto").HasMaxLength(100).IsRequired();
        builder.Property(p => p.VocesDisponiblesJson).HasColumnName("voces_disponibles").HasColumnType("jsonb");
        builder.Property(p => p.Orden).HasColumnName("orden").HasDefaultValue(0);
        builder.Property(p => p.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
        builder.Property(p => p.FechaActualizacion).HasColumnName("fecha_actualizacion").HasColumnType("timestamptz");
    }
}
