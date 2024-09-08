using VieroCodeTest;
using VieroCodeTest.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddWebApiServices(builder.Configuration);
builder.Services.AddDataInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureWebApiService(builder.Configuration);

app.UseRouting();

app.MapControllers();

app.Run();