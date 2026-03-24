using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Siembra los proveedores TTS por defecto (Azure Speech + ElevenLabs).
/// Ambos se crean deshabilitados hasta que el admin configure las API keys.
/// </summary>
public static class SembradoProveedoresTts
{
    /// <summary>
    /// Siembra los proveedores TTS si no existen.
    /// </summary>
    public static async Task SembrarAsync(AppDbContext contexto)
    {
        bool existeAzure = await contexto.ProveedoresTts
            .AnyAsync(p => p.Tipo == TipoProveedorTts.AzureSpeech);

        bool existeElevenLabs = await contexto.ProveedoresTts
            .AnyAsync(p => p.Tipo == TipoProveedorTts.ElevenLabs);

        if (!existeAzure)
        {
            contexto.ProveedoresTts.Add(new ProveedorTts
            {
                Tipo = TipoProveedorTts.AzureSpeech,
                NombreVisible = "Voces Premium (Azure)",
                Descripcion = "Voces neurales de Microsoft Azure. Alta calidad, múltiples acentos hispanos. Requiere API key de Azure Speech Services.",
                Habilitado = false,
                VozPorDefecto = "es-ES-ElviraNeural",
                Region = "westeurope",
                Orden = 1
            });
        }

        if (!existeElevenLabs)
        {
            contexto.ProveedoresTts.Add(new ProveedorTts
            {
                Tipo = TipoProveedorTts.ElevenLabs,
                NombreVisible = "Voces Ultra-Realistas (ElevenLabs)",
                Descripcion = "Voces generadas por IA de ElevenLabs. Las más naturales del mercado. Multilingüe. Requiere API key de ElevenLabs.",
                Habilitado = false,
                VozPorDefecto = "21m00Tcm4TlvDq8ikWAM",
                Orden = 2
            });
        }

        await contexto.SaveChangesAsync();
    }
}
