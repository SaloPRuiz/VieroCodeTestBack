using Microsoft.EntityFrameworkCore;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Domain.Application.Models;
using VieroCodeTest.Infra.Data.Persistence;
using VieroCodeTest.Infra.Data.Persistence.Models;

namespace VieroCodeTest.Infra.Data.Repositories;

public class AlumnoRepo : IAlumnoRepo
{
    private readonly VieroCodeContext _ctx;

    public AlumnoRepo(VieroCodeContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<AlumnoDto>> ConsultarListadoAlumnos()
    {
        var listadoAlumnos = await _ctx.Alumnos
            .Select(x => new AlumnoDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellidos = x.Apellidos,
                FechaNacimiento = x.FechaNacimiento,
                Genero = x.Genero
            }).ToListAsync();

        return listadoAlumnos;
    }

    public async Task<AlumnoDto> ConsultarAlumnoPorId(int alumnoId)
    {
        var alumno = await _ctx.Alumnos.Where(x => x.Id == alumnoId)
            .Select(x => new AlumnoDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellidos = x.Apellidos,
                FechaNacimiento = x.FechaNacimiento,
                Genero = x.Genero
            }).FirstOrDefaultAsync();

        return alumno ?? new AlumnoDto();
    }

    public async Task<AlumnoDto> UpsertAlumno(AlumnoDto alumno)
    {
        var entityToUpsert = await _ctx.Alumnos.Where(x => x.Id == alumno.Id).FirstOrDefaultAsync();

        if (entityToUpsert is null)
        {
            entityToUpsert = new Alumno
            {
                Nombre = alumno.Nombre,
                Apellidos = alumno.Apellidos,
                FechaNacimiento = alumno.FechaNacimiento,
                Genero = alumno.Genero
            };

            await _ctx.AddAsync(entityToUpsert);
        }
        else
        {
            entityToUpsert.Nombre = alumno.Nombre;
            entityToUpsert.Apellidos = alumno.Apellidos;
            entityToUpsert.FechaNacimiento = alumno.FechaNacimiento;
            entityToUpsert.Genero = alumno.Genero;
        }

        await _ctx.SaveChangesAsync();

        return alumno;
    }

    public async Task EliminarAlumno(int alumnoId)
    {
        var alumnoGrados = await _ctx.AlumnoGrados.Where(x => x.AlumnoId == alumnoId).ToListAsync();
        if (alumnoGrados.Any())
        {
            _ctx.AlumnoGrados.RemoveRange(alumnoGrados);
        }
        
        var entity = await _ctx.Alumnos.Where(x => x.Id == alumnoId).FirstOrDefaultAsync();

        if (entity is not null)
        {
            _ctx.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<List<AlumnoGradoDto>> ConsultarAlumnosPorGrado()
    {
        var listadoAlumnos = await _ctx.AlumnoGrados
            .Select(x => new AlumnoGradoDto
            {
                Id = x.Id,
                AlumnoId = x.AlumnoId,
                AlumnoNombres = $"{x.Alumno.Nombre} {x.Alumno.Apellidos}",
                GradoId = x.GradoId,
                GradoNombre = x.Grado.Nombre,
                Seccion = x.Seccion
            }).ToListAsync();

        return listadoAlumnos;
    }

    public async Task<AlumnoGradoDto> ConsultarAlumnosPorGradoPorId(int alumnoGradoId)
    {
        var alumno = await _ctx.AlumnoGrados.Where(x => x.Id == alumnoGradoId)
            .Select(x => new AlumnoGradoDto
            {
                Id = x.Id,
                AlumnoId = x.AlumnoId,
                AlumnoNombres = $"{x.Alumno.Nombre} {x.Alumno.Apellidos}",
                GradoId = x.GradoId,
                GradoNombre = x.Grado.Nombre,
                Seccion = x.Seccion
            }).FirstOrDefaultAsync();

        return alumno ?? new AlumnoGradoDto();;
    }

    public async Task<AlumnoGradoDto> UpsertAlumnoGrado(AlumnoGradoDto alumnoGradoDto)
    {
        var entityToUpsert = await _ctx.AlumnoGrados.Where(x => x.Id == alumnoGradoDto.Id).FirstOrDefaultAsync();

        if (entityToUpsert is null)
        {
            entityToUpsert = new AlumnoGrado
            {
                Seccion = alumnoGradoDto.Seccion,
                AlumnoId = alumnoGradoDto.AlumnoId,
                GradoId = alumnoGradoDto.GradoId
            };

            await _ctx.AlumnoGrados.AddAsync(entityToUpsert);
        }
        else
        {
            entityToUpsert.Seccion = alumnoGradoDto.Seccion;
            entityToUpsert.AlumnoId = alumnoGradoDto.AlumnoId;
            entityToUpsert.GradoId = alumnoGradoDto.GradoId;
        }

        await _ctx.SaveChangesAsync();

        return alumnoGradoDto;
    }

    public async Task EliminarAlumnoGrado(int alumnoGradoId)
    {
        var entity = await _ctx.AlumnoGrados.Where(x => x.Id == alumnoGradoId).FirstOrDefaultAsync();

        if (entity is not null)
        {
            _ctx.AlumnoGrados.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}