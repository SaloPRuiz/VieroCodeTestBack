using VieroCodeTest.Domain.Application.Models;

namespace VieroCodeTest.Domain.Application.Contracts.Persistence;

public interface IAlumnoRepo
{
    Task<List<AlumnoDto>> ConsultarListadoAlumnos();
    Task<AlumnoDto> ConsultarAlumnoPorId(int alumnoId);
    Task<AlumnoDto> UpsertAlumno(AlumnoDto alumno);
    Task EliminarAlumno(int alumnoId);
    
    Task<List<AlumnoGradoDto>> ConsultarAlumnosPorGrado();
    Task<AlumnoGradoDto> ConsultarAlumnosPorGradoPorId(int alumnoGradoId);
    Task<AlumnoGradoDto> UpsertAlumnoGrado(AlumnoGradoDto alumnoGradoDto);
    Task EliminarAlumnoGrado(int alumnoGradoId);
}