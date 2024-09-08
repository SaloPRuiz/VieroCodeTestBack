using Microsoft.EntityFrameworkCore;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Domain.Application.Models;
using VieroCodeTest.Infra.Data.Persistence;
using VieroCodeTest.Infra.Data.Persistence.Models;

namespace VieroCodeTest.Infra.Data.Repositories;

public class GradoRepo : IGradoRepo
{
    private readonly VieroCodeContext _ctx;

    public GradoRepo(VieroCodeContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<GradoDto>> ConsultarGrados()
    {
        var listaGrados = await _ctx.Grados
            .Select(x => new GradoDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                ProfesorId = x.ProfesorId,
                ProfesorNombre = $"{x.Profesor.Nombre} {x.Profesor.Apellidos}"
            }).ToListAsync();

        return listaGrados;
    }

    public async Task<GradoDto> ConsultarGradoPorId(int gradoId)
    {
        var entity = await _ctx.Grados.Where(x => x.Id == gradoId)
            .Select(x => new GradoDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                ProfesorId = x.ProfesorId,
                ProfesorNombre = $"{x.Profesor.Nombre} {x.Profesor.Apellidos}"
            }).FirstOrDefaultAsync();

        return entity ?? new GradoDto();
    }

    public async Task<GradoDto> UpsertGrado(GradoDto grado)
    {
        var entityToUpsert = await _ctx.Grados.Where(x => x.Id == grado.Id).FirstOrDefaultAsync();

        if (entityToUpsert is null)
        {
            entityToUpsert = new Grado
            {
                Nombre = grado.Nombre,
                ProfesorId = grado.ProfesorId
            };

            await _ctx.AddAsync(entityToUpsert);
        }
        else
        {
            entityToUpsert.Nombre = grado.Nombre;
            entityToUpsert.ProfesorId = grado.ProfesorId;
        }

        await _ctx.SaveChangesAsync();

        return grado;
    }

    public async Task EliminarGrado(int gradoId)
    {
        var entity = await _ctx.Grados.Where(x => x.Id == gradoId).FirstOrDefaultAsync();

        if (entity is not null)
        {
            _ctx.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}