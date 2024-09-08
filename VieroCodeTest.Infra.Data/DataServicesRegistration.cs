using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VieroCodeTest.Domain.Application.Contracts.Persistence;
using VieroCodeTest.Infra.Data.Repositories;

namespace VieroCodeTest.Infra.Data;

public static class DataServicesRegistration
{
    public static IServiceCollection AddDataInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAlumnoRepo, AlumnoRepo>();
        services.AddScoped<IProfesorRepo, ProfesorRepo>();
        services.AddScoped<IGradoRepo, GradoRepo>();

        return services;
    }
}