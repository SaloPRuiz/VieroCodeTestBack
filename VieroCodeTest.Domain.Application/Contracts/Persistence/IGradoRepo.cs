using VieroCodeTest.Domain.Application.Models;

namespace VieroCodeTest.Domain.Application.Contracts.Persistence;

public interface IGradoRepo
{
    Task<List<GradoDto>> ConsultarGrados();
    Task<GradoDto> ConsultarGradoPorId(int gradoId);
    Task<GradoDto> UpsertGrado(GradoDto grado);
    Task EliminarGrado(int gradoId);
}