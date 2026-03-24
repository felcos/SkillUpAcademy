using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionProveedorIA : IEntityTypeConfiguration<ProveedorIA>
{
    public void Configure(EntityTypeBuilder<ProveedorIA> builder)
    {
        builder.ToTable("proveedores_ia");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Tipo).HasColumnName("tipo").HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.HasIndex(p => p.Tipo).IsUnique();
        builder.Property(p => p.NombreVisible).HasColumnName("nombre_visible").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descripcion).HasColumnName("descripcion").HasColumnType("text");
        builder.Property(p => p.Habilitado).HasColumnName("habilitado").HasDefaultValue(false);
        builder.Property(p => p.EsActivo).HasColumnName("es_activo").HasDefaultValue(false);
        builder.Property(p => p.ApiKey).HasColumnName("api_key").HasMaxLength(500);
        builder.Property(p => p.UrlBase).HasColumnName("url_base").HasMaxLength(255).IsRequired();
        builder.Property(p => p.ModeloChat).HasColumnName("modelo_chat").HasMaxLength(100).IsRequired();
        builder.Property(p => p.MaxTokens).HasColumnName("max_tokens").HasDefaultValue(1000);
        builder.Property(p => p.Temperatura).HasColumnName("temperatura").HasDefaultValue(0.7);
        builder.Property(p => p.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
        builder.Property(p => p.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");
    }
}
