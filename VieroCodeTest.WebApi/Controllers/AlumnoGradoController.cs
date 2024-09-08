using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Domain.Application.Models;

namespace VieroCodeTest.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class AlumnoGradoController : ControllerBase
{
    private readonly IAlumnoRepo _repo;

    public AlumnoGradoController(IAlumnoRepo repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<AlumnoGradoDto>>> ConsultarAlumnosPorGrado()
    {
        try
        {
            var listaAlumnos = await _repo.ConsultarAlumnosPorGrado();
            return StatusCode(StatusCodes.Status200OK, listaAlumnos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("{alumnoId}")]
    public async Task<ActionResult<AlumnoGradoDto>> ConsultarAlumnosPorGrado([FromRoute] int alumnoId)
    {
        try
        {
            var alumno = await _repo.ConsultarAlumnosPorGradoPorId(alumnoId);
            return StatusCode(StatusCodes.Status200OK, alumno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<AlumnoGradoDto>> ActualizarAlumnosPorGrado([FromBody] AlumnoGradoDto alumnoDto)
    {
        try
        {
            var alumno = await _repo.UpsertAlumnoGrado(alumnoDto);
            return StatusCode(StatusCodes.Status200OK, alumno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("{alumnoGradoId}")]
    public async Task<ActionResult> EliminarAlumnosPorGrado([FromRoute] int alumnoGradoId)
    {
        try
        {
            await _repo.EliminarAlumnoGrado(alumnoGradoId);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}