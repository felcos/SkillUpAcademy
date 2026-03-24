namespace SkillUpAcademy.Core.DTOs.IA;

/// <summary>
/// DTO para mostrar la configuración de un proveedor de IA.
/// </summary>
public class ConfiguracionProveedorIADto
{
    /// <summary>Identificador.</summary>
    public int Id { get; set; }

    /// <summary>Tipo de proveedor (Anthropic, OpenAI, Groq, etc.).</summary>
    public string Tipo { get; set; } = string.Empty;

    /// <summary>Nombre visible.</summary>
    public string NombreVisible { get; set; } = string.Empty;

    /// <summary>Descripción corta.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Si está habilitado.</summary>
    public bool Habilitado { get; set; }

    /// <summary>Si es el proveedor activo para conversaciones.</summary>
    public bool EsActivo { get; set; }

    /// <summary>Si tiene API key configurada (no se expone la key real).</summary>
    public bool TieneApiKey { get; set; }

    /// <summary>URL base de la API.</summary>
    public string UrlBase { get; set; } = string.Empty;

    /// <summary>Modelo configurado.</summary>
    public string ModeloChat { get; set; } = string.Empty;

    /// <summary>Máximo de tokens.</summary>
    public int MaxTokens { get; set; }

    /// <summary>Temperatura.</summary>
    public double Temperatura { get; set; }
}
