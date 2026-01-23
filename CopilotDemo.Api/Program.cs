
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Servicios básicos: explorador de endpoints y Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(); // Habilitar controladores para testing
builder.Services.Configure<CopilotDemo.Api.Options.ProductsOptions>(
    builder.Configuration.GetSection("Products")
);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Copilot Demo API",
        Version = "v1",
        Description = "API mínima para demostraciones de orquestación con GitHub Copilot"
    });
});

var app = builder.Build();

// Swagger solo en Desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); // Mapear controladores

// Endpoint de ejemplo: WeatherForecast (similar al template)
string[] summaries = new[]
{
    "Caluroso", "Templado", "Frío", "Soleado", "Lluvioso", "Ventoso", "Húmedo", "Seco"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    (
        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        Random.Shared.Next(-20, 55),
        summaries[Random.Shared.Next(summaries.Length)]
    ));

    return Results.Ok(forecast);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Endpoint simple para la demo de PR
app.MapGet("/ping", () => Results.Ok(new { pong = true, at = DateTime.UtcNow }))
   .WithName("Ping")
   .WithOpenApi();

// Nuevo endpoint de health check para testing
app.MapGet("/health", () =>
{
    var healthStatus = new
    {
        status = "healthy",
        version = "1.0.0",
        timestamp = DateTime.UtcNow,
        uptime = Environment.TickCount64,
        environment = app.Environment.EnvironmentName
    };
    return Results.Ok(healthStatus);
})
.WithName("HealthCheck")
.WithOpenApi();

// Endpoint básico de usuarios para testing
app.MapGet("/users", () =>
{
    var users = new[] {
        new { id = 1, name = "Juan Pérez", email = "juan@example.com" },
        new { id = 2, name = "María García", email = "maria@example.com" },
        new { id = 3, name = "Carlos López", email = "carlos@example.com" }
    };
    return Results.Ok(users);
})
.WithName("GetUsers")
.WithOpenApi();

app.Run();

// Tipo record para el ejemplo
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
