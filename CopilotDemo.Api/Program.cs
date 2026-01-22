
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Servicios básicos: explorador de endpoints y Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Copilot Demo API",
        Version = "v1",
        Description = "API mínima para demostraciones de orquestación con GitHub Copilot"
    });
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Agregar health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// HTTPS redirection en producción
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Habilitar CORS
app.UseCors();

// Swagger solo en Desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

// Health check endpoint
app.MapHealthChecks("/health")
   .WithName("HealthCheck");

app.Run();

// Tipo record para el ejemplo
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC * 9 / 5);
}
