using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionLogro : IEntityTypeConfiguration<Logro>
{
    public void Configure(EntityTypeBuilder<Logro> builder)
    {
        builder.ToTable("logros");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnName("id");
        builder.Property(l => l.Slug).HasColumnName("slug").HasMaxLength(50).IsRequired();
        builder.HasIndex(l => l.Slug).IsUnique();
        builder.Property(l => l.Titulo).HasColumnName("titulo").HasMaxLength(100).IsRequired();
        builder.Property(l => l.Descripcion).HasColumnName("descripcion").HasMaxLength(300);
        builder.Property(l => l.Icono).HasColumnName("icono").HasMaxLength(10);
        builder.Property(l => l.PuntosRequeridos).HasColumnName("puntos_requeridos").HasDefaultValue(0);
        builder.Property(l => l.Condicion).HasColumnName("condicion").HasMaxLength(200);

        builder.HasMany(l => l.LogrosUsuario).WithOne(lu => lu.Logro).HasForeignKey(lu => lu.LogroId).OnDelete(DeleteBehavior.Cascade);
    }
}
