using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VieroCodeTest.Infra.Data.Persistence;

namespace VieroCodeTest;

public static class WebApiRegistration
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VieroCode API", Version = "v1" });
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularClient",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // El origen del frontend en Angular
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });
        
        services.AddControllers();
        
        // Services - WebApi Layer
        services.AddDbContext<VieroCodeContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("VieroCodeDB"),
                sqlServerOptionsAction: sqlserveroptions => sqlserveroptions.EnableRetryOnFailure());
        });
        

        return services;
    }

    public static void ConfigureWebApiService(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "VieroCode API V1"); });

        app.UseCors("AllowAngularClient");
        app.UseAuthentication();
    }
}