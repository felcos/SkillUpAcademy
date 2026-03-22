using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionOpcionQuiz : IEntityTypeConfiguration<OpcionQuiz>
{
    public void Configure(EntityTypeBuilder<OpcionQuiz> builder)
    {
        builder.ToTable("opciones_quiz");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("id");
        builder.Property(o => o.PreguntaQuizId).HasColumnName("pregunta_quiz_id");
        builder.Property(o => o.TextoOpcion).HasColumnName("texto_opcion").HasMaxLength(500).IsRequired();
        builder.Property(o => o.EsCorrecta).HasColumnName("es_correcta").HasDefaultValue(false);
        builder.Property(o => o.Retroalimentacion).HasColumnName("retroalimentacion").HasMaxLength(500);
        builder.Property(o => o.Orden).HasColumnName("orden").HasDefaultValue(0);
    }
}
