
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

var app = builder.Build();

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

app.Run();

// Tipo record para el ejemplo
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
