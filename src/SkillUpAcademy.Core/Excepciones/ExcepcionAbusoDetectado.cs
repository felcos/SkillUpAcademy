namespace SkillUpAcademy.Core.Excepciones;

/// <summary>
/// Se lanza cuando se detecta un intento de abuso de la IA.
/// </summary>
public class ExcepcionAbusoDetectado : Exception
{
    public string TipoViolacion { get; }
    public string AccionTomada { get; }

    public ExcepcionAbusoDetectado(string tipoViolacion, string accionTomada, string mensaje = "Se detectó contenido inapropiado")
        : base(mensaje)
    {
        TipoViolacion = tipoViolacion;
        AccionTomada = accionTomada;
    }
}
