using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionPreguntaQuiz : IEntityTypeConfiguration<PreguntaQuiz>
{
    public void Configure(EntityTypeBuilder<PreguntaQuiz> builder)
    {
        builder.ToTable("preguntas_quiz");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.LeccionId).HasColumnName("leccion_id");
        builder.Property(p => p.TextoPregunta).HasColumnName("texto_pregunta").HasColumnType("text").IsRequired();
        builder.Property(p => p.Explicacion).HasColumnName("explicacion").HasColumnType("text");
        builder.Property(p => p.Orden).HasColumnName("orden").HasDefaultValue(0);

        builder.HasMany(p => p.Opciones).WithOne(o => o.PreguntaQuiz).HasForeignKey(o => o.PreguntaQuizId).OnDelete(DeleteBehavior.Cascade);
    }
}
