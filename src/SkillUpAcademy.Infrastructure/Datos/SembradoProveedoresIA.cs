using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;

namespace SkillUpAcademy.Infrastructure.Datos;

/// <summary>
/// Sembrado de proveedores de IA disponibles.
/// </summary>
public static class SembradoProveedoresIA
{
    /// <summary>
    /// Siembra los proveedores de IA si no existen.
    /// </summary>
    public static async Task SembrarAsync(AppDbContext contexto)
    {
        if (await contexto.ProveedoresIA.AnyAsync())
            return;

        List<ProveedorIA> proveedores = new()
        {
            new ProveedorIA
            {
                Tipo = TipoProveedorIA.Anthropic,
                NombreVisible = "Claude - Anthropic",
                Descripcion = "Modelos Claude de Anthropic. Excelente en razonamiento, escritura y seguimiento de instrucciones.",
                Habilitado = true,
                EsActivo = true,
                UrlBase = "https://api.anthropic.com/v1",
                ModeloChat = "claude-sonnet-4-20250514",
                MaxTokens = 1000,
                Temperatura = 0.7
            },
            new ProveedorIA
            {
                Tipo = TipoProveedorIA.OpenAI,
                NombreVisible = "GPT - OpenAI",
                Descripcion = "Modelos GPT de OpenAI. Versátil y con amplio ecosistema.",
                Habilitado = false,
                EsActivo = false,
                UrlBase = "https://api.openai.com/v1",
                ModeloChat = "gpt-4o",
                MaxTokens = 1000,
                Temperatura = 0.7
            },
            new ProveedorIA
            {
                Tipo = TipoProveedorIA.Groq,
                NombreVisible = "Groq",
                Descripcion = "Inferencia ultra-rápida con modelos open-source (Llama, Mixtral). Ideal para baja latencia.",
                Habilitado = false,
                EsActivo = false,
                UrlBase = "https://api.groq.com/openai/v1",
                ModeloChat = "llama-3.1-70b-versatile",
                MaxTokens = 1000,
                Temperatura = 0.7
            },
            new ProveedorIA
            {
                Tipo = TipoProveedorIA.Mistral,
                NombreVisible = "Mistral AI",
                Descripcion = "Modelos Mistral/Mixtral. Buena relación calidad-precio, buen soporte multilingüe.",
                Habilitado = false,
                EsActivo = false,
                UrlBase = "https://api.mistral.ai/v1",
                ModeloChat = "mistral-large-latest",
                MaxTokens = 1000,
                Temperatura = 0.7
            },
            new ProveedorIA
            {
                Tipo = TipoProveedorIA.Google,
                NombreVisible = "Gemini - Google",
                Descripcion = "Modelos Gemini de Google. Fuerte en razonamiento multimodal y contexto largo.",
                Habilitado = false,
                EsActivo = false,
                UrlBase = "https://generativelanguage.googleapis.com/v1beta",
                ModeloChat = "gemini-2.0-flash",
                MaxTokens = 1000,
                Temperatura = 0.7
            }
        };

        contexto.ProveedoresIA.AddRange(proveedores);
        await contexto.SaveChangesAsync();
    }
}
