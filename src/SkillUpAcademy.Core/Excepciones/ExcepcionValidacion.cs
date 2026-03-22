namespace SkillUpAcademy.Core.Excepciones;

/// <summary>
/// Se lanza cuando hay un error de validación.
/// </summary>
public class ExcepcionValidacion : Exception
{
    public IReadOnlyDictionary<string, string[]> Errores { get; }

    public ExcepcionValidacion(string mensaje) : base(mensaje)
    {
        Errores = new Dictionary<string, string[]>();
    }

    public ExcepcionValidacion(IDictionary<string, string[]> errores)
        : base("Se produjeron uno o más errores de validación")
    {
        Errores = new Dictionary<string, string[]>(errores);
    }
}
