using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Configuración de un proveedor de IA (Anthropic, OpenAI, Groq, etc.).
/// </summary>
public class ProveedorIA
{
    public int Id { get; set; }

    /// <summary>Tipo de proveedor.</summary>
    public TipoProveedorIA Tipo { get; set; }

    /// <summary>Nombre visible para el admin (ej: "Claude - Anthropic").</summary>
    public string NombreVisible { get; set; } = string.Empty;

    /// <summary>Descripción corta del proveedor.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Indica si está habilitado por el admin.</summary>
    public bool Habilitado { get; set; }

    /// <summary>Indica si es el proveedor activo para las conversaciones.</summary>
    public bool EsActivo { get; set; }

    /// <summary>API key del proveedor.</summary>
    public string? ApiKey { get; set; }

    /// <summary>URL base de la API (ej: https://api.anthropic.com/v1).</summary>
    public string UrlBase { get; set; } = string.Empty;

    /// <summary>Modelo a usar (ej: claude-sonnet-4-20250514, gpt-4o, llama-3.1-70b).</summary>
    public string ModeloChat { get; set; } = string.Empty;

    /// <summary>Máximo de tokens en la respuesta.</summary>
    public int MaxTokens { get; set; } = 1000;

    /// <summary>Temperatura (0.0 - 1.0).</summary>
    public double Temperatura { get; set; } = 0.7;

    /// <summary>Fecha de creación.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    /// <summary>Fecha de última modificación.</summary>
    public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
}
