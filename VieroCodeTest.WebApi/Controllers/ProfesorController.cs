using Microsoft.AspNetCore.Mvc;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Domain.Application.Models;

namespace VieroCodeTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesorController : ControllerBase
{
    private readonly IProfesorRepo _repo;

    public ProfesorController(IProfesorRepo repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ProfesorDto>>> ConsultarListaProfesores()
    {
        try
        {
            var listaProfesores = await _repo.ConsultarListadoProfesores();
            return StatusCode(StatusCodes.Status200OK, listaProfesores);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("{profesorId}")]
    public async Task<ActionResult<ProfesorDto>> ConsultarProfesor([FromRoute] int profesorId)
    {
        try
        {
            var profesor = await _repo.ConsultarProfesorPorId(profesorId);
            return StatusCode(StatusCodes.Status200OK, profesor);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<ProfesorDto>> ActualizarProfesor([FromBody] ProfesorDto profesorDto)
    {
        try
        {
            var profesor = await _repo.UpsertProfesor(profesorDto);
            return StatusCode(StatusCodes.Status200OK, profesor);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("{profesorId}")]
    public async Task<ActionResult> EliminarProfesor([FromRoute] int profesorId)
    {
        try
        {
            await _repo.EliminarProfesor(profesorId);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}