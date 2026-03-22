namespace SkillUpAcademy.Core.Excepciones;

/// <summary>
/// Se lanza cuando un recurso no se encuentra.
/// </summary>
public class ExcepcionNoEncontrado : Exception
{
    public string NombreRecurso { get; }
    public object? ClaveRecurso { get; }

    public ExcepcionNoEncontrado(string nombreRecurso, object? claveRecurso = null)
        : base($"{nombreRecurso} no encontrado{(claveRecurso != null ? $" con clave '{claveRecurso}'" : "")}")
    {
        NombreRecurso = nombreRecurso;
        ClaveRecurso = claveRecurso;
    }
}
