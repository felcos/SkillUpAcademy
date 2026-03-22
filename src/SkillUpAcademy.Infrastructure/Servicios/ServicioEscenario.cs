using Microsoft.EntityFrameworkCore;
using SkillUpAcademy.Core.DTOs.Escenario;
using SkillUpAcademy.Core.Entidades;
using SkillUpAcademy.Core.Enums;
using SkillUpAcademy.Core.Excepciones;
using SkillUpAcademy.Core.Interfaces.Repositorios;
using SkillUpAcademy.Core.Interfaces.Servicios;
using SkillUpAcademy.Infrastructure.Datos;

namespace SkillUpAcademy.Infrastructure.Servicios;

/// <summary>
/// Servicio de escenarios interactivos.
/// </summary>
public class ServicioEscenario(
    IRepositorioLecciones _repositorioLecciones,
    IRepositorioProgreso _repositorioProgreso,
    AppDbContext _contexto) : IServicioEscenario
{
    /// <inheritdoc />
    public async Task<EscenarioDto> ObtenerEscenarioAsync(int leccionId)
    {
        Leccion? leccion = await _repositorioLecciones.ObtenerConEscenarioAsync(leccionId);
        if (leccion == null)
            throw new ExcepcionNoEncontrado("Leccion", leccionId);

        Escenario? escenario = leccion.Escenarios.FirstOrDefault();
        if (escenario == null)
            throw new ExcepcionNoEncontrado("Escenario en leccion", leccionId);

        return new EscenarioDto
        {
            Id = escenario.Id,
            TextoSituacion = escenario.TextoSituacion,
            Contexto = escenario.Contexto,
            GuionAudio = escenario.GuionAudio,
            Opciones = escenario.Opciones.Select(o => new OpcionEscenarioDto
            {
                Id = o.Id,
                TextoOpcion = o.TextoOpcion,
                Orden = o.Orden
            }).ToList()
        };
    }

    /// <inheritdoc />
    public async Task<ResultadoEscenarioDto> ElegirOpcionAsync(int leccionId, PeticionEleccionEscenario peticion, Guid usuarioId)
    {
        OpcionEscenario? opcion = await _contexto.OpcionesEscenario
            .Include(o => o.SiguienteEscenario)
                .ThenInclude(s => s!.Opciones.OrderBy(op => op.Orden))
            .FirstOrDefaultAsync(o => o.Id == peticion.OpcionId && o.EscenarioId == peticion.EscenarioId);

        if (opcion == null)
            throw new ExcepcionNoEncontrado("OpcionEscenario", peticion.OpcionId);

        // Guardar elección
        EleccionEscenarioUsuario eleccion = new EleccionEscenarioUsuario
        {
            UsuarioId = usuarioId,
            EscenarioId = peticion.EscenarioId,
            OpcionEscenarioId = peticion.OpcionId,
            FechaEleccion = DateTime.UtcNow
        };
        _contexto.EleccionesEscenarioUsuario.Add(eleccion);
        await _contexto.SaveChangesAsync();

        // Actualizar progreso
        ProgresoUsuario progreso = new ProgresoUsuario
        {
            UsuarioId = usuarioId,
            LeccionId = leccionId,
            Estado = opcion.SiguienteEscenarioId == null ? EstadoProgreso.Completado : EstadoProgreso.EnProgreso,
            Puntuacion = opcion.PuntosOtorgados,
            FechaCompletado = opcion.SiguienteEscenarioId == null ? DateTime.UtcNow : null,
            UltimoAcceso = DateTime.UtcNow
        };
        await _repositorioProgreso.CrearOActualizarAsync(progreso);

        EscenarioDto? siguienteEscenario = null;
        if (opcion.SiguienteEscenario != null)
        {
            siguienteEscenario = new EscenarioDto
            {
                Id = opcion.SiguienteEscenario.Id,
                TextoSituacion = opcion.SiguienteEscenario.TextoSituacion,
                Contexto = opcion.SiguienteEscenario.Contexto,
                GuionAudio = opcion.SiguienteEscenario.GuionAudio,
                Opciones = opcion.SiguienteEscenario.Opciones.Select(o => new OpcionEscenarioDto
                {
                    Id = o.Id,
                    TextoOpcion = o.TextoOpcion,
                    Orden = o.Orden
                }).ToList()
            };
        }

        return new ResultadoEscenarioDto
        {
            TipoResultado = opcion.TipoResultado.ToString(),
            TextoRetroalimentacion = opcion.TextoRetroalimentacion,
            PuntosOtorgados = opcion.PuntosOtorgados,
            SiguienteEscenario = siguienteEscenario
        };
    }
}
