using Microsoft.AspNetCore.Mvc;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Domain.Application.Models;

namespace VieroCodeTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradoController : ControllerBase
{
    private readonly IGradoRepo _repo;

    public GradoController(IGradoRepo repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GradoDto>>> ConsultarListaGrados()
    {
        try
        {
            var listaGrados = await _repo.ConsultarGrados();
            return StatusCode(StatusCodes.Status200OK, listaGrados);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("{gradoId}")]
    public async Task<ActionResult<GradoDto>> ConsultarGrador([FromRoute] int gradoId)
    {
        try
        {
            var grado = await _repo.ConsultarGradoPorId(gradoId);
            return StatusCode(StatusCodes.Status200OK, grado);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<GradoDto>> ActualizarGradp([FromBody] GradoDto gradoDto)
    {
        try
        {
            var grado = await _repo.UpsertGrado(gradoDto);
            return StatusCode(StatusCodes.Status200OK, grado);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("{gradoId}")]
    public async Task<ActionResult> EliminarGrado([FromRoute] int gradoId)
    {
        try
        {
            await _repo.EliminarGrado(gradoId);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}