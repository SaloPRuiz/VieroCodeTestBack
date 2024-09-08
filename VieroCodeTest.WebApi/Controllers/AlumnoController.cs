using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Domain.Application.Models;

namespace VieroCodeTest.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class AlumnoController : ControllerBase
{
    private readonly IAlumnoRepo _repo;

    public AlumnoController(IAlumnoRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<AlumnoDto>>> ConsultarAlumnos()
    {
        try
        {
            var listaAlumnos = await _repo.ConsultarListadoAlumnos();
            return StatusCode(StatusCodes.Status200OK, listaAlumnos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("{alumnoId}")]
    public async Task<ActionResult<AlumnoDto>> ConsultarAlumno([FromRoute] int alumnoId)
    {
        try
        {
            var alumno = await _repo.ConsultarAlumnoPorId(alumnoId);
            return StatusCode(StatusCodes.Status200OK, alumno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<AlumnoDto>> ActualizarAlumno([FromBody] AlumnoDto alumnoDto)
    {
        try
        {
            var alumno = await _repo.UpsertAlumno(alumnoDto);
            return StatusCode(StatusCodes.Status200OK, alumno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("{idAlumno}")]
    public async Task<ActionResult> EliminarAlumno([FromRoute] int idAlumno)
    {
        try
        {
            await _repo.EliminarAlumno(idAlumno);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
}