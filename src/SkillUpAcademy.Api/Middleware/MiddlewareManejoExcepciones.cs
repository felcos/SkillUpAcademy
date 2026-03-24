using System.Net;
using System.Text.Json;
using SkillUpAcademy.Core.Excepciones;

namespace SkillUpAcademy.Api.Middleware;

/// <summary>
/// Middleware global para manejo de excepciones.
/// Convierte excepciones del dominio en respuestas HTTP apropiadas.
/// </summary>
public class MiddlewareManejoExcepciones(RequestDelegate _siguiente, ILogger<MiddlewareManejoExcepciones> _logger)
{
    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await _siguiente(contexto);
        }
        catch (Exception excepcion)
        {
            await ManejarExcepcionAsync(contexto, excepcion);
        }
    }

    private async Task ManejarExcepcionAsync(HttpContext contexto, Exception excepcion)
    {
        HttpStatusCode codigoEstado = excepcion switch
        {
            ExcepcionNoEncontrado => HttpStatusCode.NotFound,
            ExcepcionNoAutorizado => HttpStatusCode.Forbidden,
            ExcepcionValidacion => HttpStatusCode.BadRequest,
            ExcepcionAbusoDetectado => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.InternalServerError
        };

        string codigoError = excepcion switch
        {
            ExcepcionNoEncontrado => "RECURSO_NO_ENCONTRADO",
            ExcepcionNoAutorizado => "NO_AUTORIZADO",
            ExcepcionValidacion => "ERROR_VALIDACION",
            ExcepcionAbusoDetectado => "ABUSO_DETECTADO",
            _ => "ERROR_INTERNO"
        };

        if (codigoEstado == HttpStatusCode.InternalServerError)
        {
            _logger.LogError(excepcion, "Error interno no controlado: {Mensaje}", excepcion.Message);
        }
        else if (codigoEstado != HttpStatusCode.BadRequest)
        {
            _logger.LogWarning("Excepción controlada [{Codigo}]: {Mensaje}", codigoError, excepcion.Message);
        }

        contexto.Response.ContentType = "application/json";
        contexto.Response.StatusCode = (int)codigoEstado;

        object respuesta = new
        {
            error = new
            {
                code = codigoError,
                message = codigoEstado == HttpStatusCode.InternalServerError
                    ? "Ha ocurrido un error interno. Inténtalo de nuevo más tarde."
                    : excepcion.Message,
                details = excepcion is ExcepcionValidacion validacion
                    ? validacion.Errores
                    : null
            }
        };

        JsonSerializerOptions opciones = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        string json = JsonSerializer.Serialize(respuesta, opciones);
        await contexto.Response.WriteAsync(json);
    }
}
