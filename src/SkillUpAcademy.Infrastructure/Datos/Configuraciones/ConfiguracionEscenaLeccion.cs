using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillUpAcademy.Core.Entidades;

namespace SkillUpAcademy.Infrastructure.Datos.Configuraciones;

public class ConfiguracionEscenaLeccion : IEntityTypeConfiguration<EscenaLeccion>
{
    public void Configure(EntityTypeBuilder<EscenaLeccion> builder)
    {
        builder.ToTable("escenas_leccion");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.LeccionId).HasColumnName("leccion_id");
        builder.Property(e => e.Orden).HasColumnName("orden");
        builder.Property(e => e.TipoContenidoVisual).HasColumnName("tipo_contenido_visual").HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.TituloEscena).HasColumnName("titulo_escena").HasMaxLength(200);
        builder.Property(e => e.GuionAria).HasColumnName("guion_aria").HasColumnType("text");
        builder.Property(e => e.ContenidoVisual).HasColumnName("contenido_visual").HasColumnType("text");
        builder.Property(e => e.MetadatosVisuales).HasColumnName("metadatos_visuales").HasColumnType("jsonb");
        builder.Property(e => e.TransicionEntrada).HasColumnName("transicion_entrada").HasConversion<string>().HasMaxLength(20);
        builder.Property(e => e.Layout).HasColumnName("layout").HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.DuracionSegundos).HasColumnName("duracion_segundos").HasDefaultValue(15);
        builder.Property(e => e.EsPausaReflexiva).HasColumnName("es_pausa_reflexiva").HasDefaultValue(false);
        builder.Property(e => e.SegundosPausa).HasColumnName("segundos_pausa").HasDefaultValue(0);

        builder.HasIndex(e => new { e.LeccionId, e.Orden });
        builder.HasMany(e => e.Recursos).WithOne(r => r.EscenaLeccion).HasForeignKey(r => r.EscenaLeccionId).OnDelete(DeleteBehavior.SetNull);
    }
}
