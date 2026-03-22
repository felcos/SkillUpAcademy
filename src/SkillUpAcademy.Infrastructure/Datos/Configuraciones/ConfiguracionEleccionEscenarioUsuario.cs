using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionEleccionEscenarioUsuario : IEntityTypeConfiguration<EleccionEscenarioUsuario>
{
    public void Configure(EntityTypeBuilder<EleccionEscenarioUsuario> builder)
    {
        builder.ToTable("elecciones_escenario_usuario");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        builder.Property(e => e.EscenarioId).HasColumnName("escenario_id");
        builder.Property(e => e.OpcionEscenarioId).HasColumnName("opcion_escenario_id");
        builder.Property(e => e.FechaEleccion).HasColumnName("fecha_eleccion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasOne(e => e.Usuario).WithMany(u => u.EleccionesEscenario).HasForeignKey(e => e.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.Escenario).WithMany().HasForeignKey(e => e.EscenarioId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.OpcionEscenario).WithMany().HasForeignKey(e => e.OpcionEscenarioId).OnDelete(DeleteBehavior.Cascade);
    }
}
