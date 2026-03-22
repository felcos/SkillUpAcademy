namespace SkillUpAcademy.Api.Middleware;

/// <summary>
/// Middleware para agregar cabeceras de seguridad HTTP.
/// </summary>
public class MiddlewareCabecerasSeguridad(RequestDelegate _siguiente)
{
    public async Task InvokeAsync(HttpContext contexto)
    {
        contexto.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        contexto.Response.Headers.Append("X-Frame-Options", "DENY");
        contexto.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
        contexto.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        contexto.Response.Headers.Append("Permissions-Policy", "camera=(), microphone=(), geolocation=()");

        if (!contexto.Request.Path.StartsWithSegments("/swagger"))
        {
            contexto.Response.Headers.Append("Content-Security-Policy",
                "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data:; font-src 'self'");
        }

        await _siguiente(contexto);
    }
}
