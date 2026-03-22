using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionLogroUsuario : IEntityTypeConfiguration<LogroUsuario>
{
    public void Configure(EntityTypeBuilder<LogroUsuario> builder)
    {
        builder.ToTable("logros_usuario");
        builder.HasKey(lu => lu.Id);
        builder.Property(lu => lu.Id).HasColumnName("id");
        builder.Property(lu => lu.UsuarioId).HasColumnName("usuario_id");
        builder.Property(lu => lu.LogroId).HasColumnName("logro_id");
        builder.Property(lu => lu.FechaDesbloqueo).HasColumnName("fecha_desbloqueo").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasIndex(lu => new { lu.UsuarioId, lu.LogroId }).IsUnique();
        builder.HasOne(lu => lu.Usuario).WithMany(u => u.Logros).HasForeignKey(lu => lu.UsuarioId).OnDelete(DeleteBehavior.Cascade);
    }
}
