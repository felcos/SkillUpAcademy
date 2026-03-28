using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionLeccion : IEntityTypeConfiguration<Leccion>
{
    public void Configure(EntityTypeBuilder<Leccion> builder)
    {
        builder.ToTable("lecciones");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnName("id");
        builder.Property(l => l.NivelId).HasColumnName("nivel_id");
        builder.Property(l => l.TipoLeccion).HasColumnName("tipo_leccion").HasConversion<string>().HasMaxLength(30);
        builder.Property(l => l.Titulo).HasColumnName("titulo").HasMaxLength(200).IsRequired();
        builder.Property(l => l.Descripcion).HasColumnName("descripcion").HasMaxLength(500);
        builder.Property(l => l.Contenido).HasColumnName("contenido").HasColumnType("text");
        builder.Property(l => l.PuntosClave).HasColumnName("puntos_clave").HasColumnType("jsonb");
        builder.Property(l => l.GuionAudio).HasColumnName("guion_audio").HasColumnType("text");
        builder.Property(l => l.PuntosRecompensa).HasColumnName("puntos_recompensa").HasDefaultValue(10);
        builder.Property(l => l.Orden).HasColumnName("orden").HasDefaultValue(0);
        builder.Property(l => l.DuracionMinutos).HasColumnName("duracion_minutos").HasDefaultValue(5);
        builder.Property(l => l.Activo).HasColumnName("activo").HasDefaultValue(true);
        builder.Property(l => l.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasIndex(l => l.NivelId);
        builder.HasMany(l => l.PreguntasQuiz).WithOne(p => p.Leccion).HasForeignKey(p => p.LeccionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(l => l.Escenarios).WithOne(e => e.Leccion).HasForeignKey(e => e.LeccionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(l => l.Escenas).WithOne(e => e.Leccion).HasForeignKey(e => e.LeccionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(l => l.Progresos).WithOne(p => p.Leccion).HasForeignKey(p => p.LeccionId).OnDelete(DeleteBehavior.Cascade);
    }
}
