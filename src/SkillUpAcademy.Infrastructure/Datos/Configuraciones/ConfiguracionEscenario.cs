using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionEscenario : IEntityTypeConfiguration<Escenario>
{
    public void Configure(EntityTypeBuilder<Escenario> builder)
    {
        builder.ToTable("escenarios");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.LeccionId).HasColumnName("leccion_id");
        builder.Property(e => e.TextoSituacion).HasColumnName("texto_situacion").HasColumnType("text").IsRequired();
        builder.Property(e => e.Contexto).HasColumnName("contexto").HasColumnType("text");
        builder.Property(e => e.GuionAudio).HasColumnName("guion_audio").HasColumnType("text");

        builder.HasMany(e => e.Opciones).WithOne(o => o.Escenario).HasForeignKey(o => o.EscenarioId).OnDelete(DeleteBehavior.Cascade);
    }
}
