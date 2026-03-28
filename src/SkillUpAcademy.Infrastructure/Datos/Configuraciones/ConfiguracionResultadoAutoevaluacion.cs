using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

/// <summary>
/// Configuración Fluent API para ResultadoAutoevaluacion.
/// </summary>
public class ConfiguracionResultadoAutoevaluacion : IEntityTypeConfiguration<ResultadoAutoevaluacion>
{
    public void Configure(EntityTypeBuilder<ResultadoAutoevaluacion> builder)
    {
        builder.ToTable("resultados_autoevaluacion");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.UsuarioId).HasColumnName("usuario_id");
        builder.Property(r => r.LeccionId).HasColumnName("leccion_id");
        builder.Property(r => r.RespuestasJson).HasColumnName("respuestas_json").HasColumnType("jsonb");
        builder.Property(r => r.NivelDetectado).HasColumnName("nivel_detectado").HasMaxLength(50).IsRequired();
        builder.Property(r => r.ResumenIA).HasColumnName("resumen_ia").HasColumnType("text");
        builder.Property(r => r.FechaEvaluacion).HasColumnName("fecha_evaluacion").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasIndex(r => r.UsuarioId);
        builder.HasIndex(r => new { r.UsuarioId, r.LeccionId });

        builder.HasOne(r => r.Usuario).WithMany(u => u.Autoevaluaciones).HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.Leccion).WithMany(l => l.Autoevaluaciones).HasForeignKey(r => r.LeccionId).OnDelete(DeleteBehavior.Cascade);
    }
}
