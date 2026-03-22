using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionNivel : IEntityTypeConfiguration<Nivel>
{
    public void Configure(EntityTypeBuilder<Nivel> builder)
    {
        builder.ToTable("niveles");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasColumnName("id");
        builder.Property(n => n.AreaHabilidadId).HasColumnName("area_habilidad_id");
        builder.Property(n => n.NumeroNivel).HasColumnName("numero_nivel");
        builder.Property(n => n.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
        builder.Property(n => n.Descripcion).HasColumnName("descripcion").HasMaxLength(500);
        builder.Property(n => n.PuntosDesbloqueo).HasColumnName("puntos_desbloqueo").HasDefaultValue(0);
        builder.Property(n => n.Activo).HasColumnName("activo").HasDefaultValue(true);

        builder.HasIndex(n => new { n.AreaHabilidadId, n.NumeroNivel }).IsUnique();
        builder.HasMany(n => n.Lecciones).WithOne(l => l.Nivel).HasForeignKey(l => l.NivelId).OnDelete(DeleteBehavior.Cascade);
    }
}
