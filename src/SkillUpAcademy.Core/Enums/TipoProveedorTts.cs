namespace SkillUpAcademy.Core.Enums;

/// <summary>
/// Proveedores de Text-to-Speech disponibles.
/// </summary>
public enum TipoProveedorTts
{
    /// <summary>Voz del navegador (Web Speech API). Sin coste, calidad básica.</summary>
    WebSpeechApi,

    /// <summary>Microsoft Azure Speech Services. Voces neurales de alta calidad.</summary>
    AzureSpeech,

    /// <summary>ElevenLabs. Voces ultra-realistas con clonado disponible.</summary>
    ElevenLabs
}
