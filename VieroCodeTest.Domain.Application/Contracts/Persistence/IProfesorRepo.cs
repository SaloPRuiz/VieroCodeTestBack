using VieroCodeTest.Domain.Application.Models;

namespace VieroCodeTest.Domain.Application.Contracts.Persistence;

public interface IProfesorRepo
{
    Task<List<ProfesorDto>> ConsultarListadoProfesores();
    Task<ProfesorDto> ConsultarProfesorPorId(int profesorId);
    Task<ProfesorDto> UpsertProfesor(ProfesorDto alumno);
    Task EliminarProfesor(int profesorId);
}