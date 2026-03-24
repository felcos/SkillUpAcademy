namespace SkillUpAcademy.Core.DTOs.IA;

/// <summary>
/// Petición para crear o actualizar un proveedor de IA.
/// </summary>
public class PeticionGuardarProveedorIA
{
    /// <summary>Nombre visible.</summary>
    public string NombreVisible { get; set; } = string.Empty;

    /// <summary>Descripción corta.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Si está habilitado.</summary>
    public bool Habilitado { get; set; }

    /// <summary>API key (null para mantener la existente).</summary>
    public string? ApiKey { get; set; }

    /// <summary>URL base de la API.</summary>
    public string UrlBase { get; set; } = string.Empty;

    /// <summary>Modelo a usar.</summary>
    public string ModeloChat { get; set; } = string.Empty;

    /// <summary>Máximo de tokens.</summary>
    public int MaxTokens { get; set; } = 1000;

    /// <summary>Temperatura.</summary>
    public double Temperatura { get; set; } = 0.7;
}
