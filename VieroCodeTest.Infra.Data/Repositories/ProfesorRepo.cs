using Microsoft.EntityFrameworkCore;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Domain.Application.Models;
using VieroCodeTest.Infra.Data.Persistence;
using VieroCodeTest.Infra.Data.Persistence.Models;

namespace VieroCodeTest.Infra.Data.Repositories;

public class ProfesorRepo : IProfesorRepo
{
    private readonly VieroCodeContext _ctx;

    public ProfesorRepo(VieroCodeContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<List<ProfesorDto>> ConsultarListadoProfesores()
    {
        var listaProfesores = await _ctx.Profesores
            .Select(x => new ProfesorDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellidos = x.Apellidos,
                Genero = x.Genero
            }).ToListAsync();

        return listaProfesores;
    }

    public async Task<ProfesorDto> ConsultarProfesorPorId(int profesorId)
    {
        var entity = await _ctx.Profesores.Where(x => x.Id == profesorId)
            .Select(x => new ProfesorDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellidos = x.Apellidos,
                Genero = x.Genero
            }).FirstOrDefaultAsync();

        return entity ?? new ProfesorDto();
    }

    public async Task<ProfesorDto> UpsertProfesor(ProfesorDto alumno)
    {
        var entityToUpsert = await _ctx.Profesores.Where(x => x.Id == alumno.Id).FirstOrDefaultAsync();

        if (entityToUpsert is null)
        {
            entityToUpsert = new Profesor
            {
                Nombre = alumno.Nombre,
                Apellidos = alumno.Apellidos,
                Genero = alumno.Genero
            };

            await _ctx.AddAsync(entityToUpsert);
        }
        else
        {
            entityToUpsert.Nombre = alumno.Nombre;
            entityToUpsert.Apellidos = alumno.Apellidos;
            entityToUpsert.Genero = alumno.Genero;
        }

        await _ctx.SaveChangesAsync();

        return alumno;
    }

    public async Task EliminarProfesor(int profesorId)
    {
        var grados = await _ctx.Grados.Where(x => x.ProfesorId == profesorId).ToListAsync();

        if (grados.Any())
        {
            var gradoIds = grados.Select(g => g.Id).ToList();
            var alumnoGrados = await _ctx.AlumnoGrados.Where(x => gradoIds.Contains(x.GradoId)).ToListAsync();
            if (alumnoGrados.Any())
            {
                _ctx.AlumnoGrados.RemoveRange(alumnoGrados);
            }
            
            _ctx.Grados.RemoveRange(grados);
        }
        
        var entity = await _ctx.Profesores.Where(x => x.Id == profesorId).FirstOrDefaultAsync();

        if (entity is not null)
        {
            _ctx.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}