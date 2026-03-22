using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionOpcionEscenario : IEntityTypeConfiguration<OpcionEscenario>
{
    public void Configure(EntityTypeBuilder<OpcionEscenario> builder)
    {
        builder.ToTable("opciones_escenario");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("id");
        builder.Property(o => o.EscenarioId).HasColumnName("escenario_id");
        builder.Property(o => o.TextoOpcion).HasColumnName("texto_opcion").HasMaxLength(500).IsRequired();
        builder.Property(o => o.TipoResultado).HasColumnName("tipo_resultado").HasConversion<string>().HasMaxLength(20);
        builder.Property(o => o.TextoRetroalimentacion).HasColumnName("texto_retroalimentacion").HasColumnType("text").IsRequired();
        builder.Property(o => o.PuntosOtorgados).HasColumnName("puntos_otorgados").HasDefaultValue(0);
        builder.Property(o => o.SiguienteEscenarioId).HasColumnName("siguiente_escenario_id");
        builder.Property(o => o.Orden).HasColumnName("orden").HasDefaultValue(0);

        builder.HasOne(o => o.SiguienteEscenario).WithMany().HasForeignKey(o => o.SiguienteEscenarioId).OnDelete(DeleteBehavior.SetNull);
    }
}
