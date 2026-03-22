namespace SkillUpAcademy.Core.Excepciones;

/// <summary>
/// Se lanza cuando el usuario no tiene permisos.
/// </summary>
public class ExcepcionNoAutorizado : Exception
{
    public ExcepcionNoAutorizado(string mensaje = "No tiene permisos para realizar esta acción")
        : base(mensaje)
    {
    }
}
