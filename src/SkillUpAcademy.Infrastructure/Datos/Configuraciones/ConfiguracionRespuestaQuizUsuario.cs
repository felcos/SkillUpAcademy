using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionRespuestaQuizUsuario : IEntityTypeConfiguration<RespuestaQuizUsuario>
{
    public void Configure(EntityTypeBuilder<RespuestaQuizUsuario> builder)
    {
        builder.ToTable("respuestas_quiz_usuario");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.UsuarioId).HasColumnName("usuario_id");
        builder.Property(r => r.PreguntaQuizId).HasColumnName("pregunta_quiz_id");
        builder.Property(r => r.OpcionSeleccionadaId).HasColumnName("opcion_seleccionada_id");
        builder.Property(r => r.EsCorrecta).HasColumnName("es_correcta");
        builder.Property(r => r.FechaRespuesta).HasColumnName("fecha_respuesta").HasColumnType("timestamptz").HasDefaultValueSql("NOW()");

        builder.HasIndex(r => r.UsuarioId);
        builder.HasOne(r => r.Usuario).WithMany(u => u.RespuestasQuiz).HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.PreguntaQuiz).WithMany().HasForeignKey(r => r.PreguntaQuizId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.OpcionSeleccionada).WithMany().HasForeignKey(r => r.OpcionSeleccionadaId).OnDelete(DeleteBehavior.Cascade);
    }
}
