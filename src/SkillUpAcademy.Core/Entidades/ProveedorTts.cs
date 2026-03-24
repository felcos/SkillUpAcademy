using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Core.Entidades;

/// <summary>
/// Configuración de un proveedor de TTS, gestionable desde el panel admin.
/// </summary>
public class ProveedorTts
{
    public int Id { get; set; }

    /// <summary>Tipo de proveedor (Azure, ElevenLabs, WebSpeech).</summary>
    public TipoProveedorTts Tipo { get; set; }

    /// <summary>Nombre visible para el usuario (ej: "Voces Premium", "Voces Naturales").</summary>
    public string NombreVisible { get; set; } = string.Empty;

    /// <summary>Descripción corta del proveedor.</summary>
    public string? Descripcion { get; set; }

    /// <summary>Indica si está habilitado por el admin.</summary>
    public bool Habilitado { get; set; }

    /// <summary>API key del proveedor (Azure Speech Key o ElevenLabs API Key).</summary>
    public string? ApiKey { get; set; }

    /// <summary>Región del servicio (solo Azure: westeurope, eastus, etc.).</summary>
    public string? Region { get; set; }

    /// <summary>Voz por defecto de este proveedor.</summary>
    public string VozPorDefecto { get; set; } = string.Empty;

    /// <summary>Lista de voces disponibles como JSON (cacheadas del proveedor).</summary>
    public string? VocesDisponiblesJson { get; set; }

    /// <summary>Orden de prioridad (menor = preferido). Si hay dos habilitados, se usa el de menor orden.</summary>
    public int Orden { get; set; }

    /// <summary>Fecha de creación.</summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    /// <summary>Fecha de última actualización.</summary>
    public DateTime? FechaActualizacion { get; set; }
}
