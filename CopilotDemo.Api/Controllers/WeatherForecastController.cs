using Microsoft.AspNetCore.Mvc;

namespace CopilotDemo.Api.Controllers;

/// <summary>
/// Controlador de ejemplo para demostración de workflows
/// Este controlador está incluido para testing de los agentes de GitHub Actions
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Obtiene el pronóstico del tiempo para los próximos días
    /// </summary>
    /// <returns>Lista de pronósticos</returns>
    [HttpGet]
    public ActionResult<IEnumerable<WeatherForecast>> Get()
    {
        _logger.LogInformation("Weather forecast requested at {Time}", DateTime.UtcNow);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    /// <summary>
    /// Obtiene el pronóstico para una fecha específica
    /// </summary>
    /// <param name="date">Fecha del pronóstico</param>
    /// <returns>Pronóstico para la fecha solicitada</returns>
    [HttpGet("{date:datetime}")]
    public ActionResult<WeatherForecast> GetByDate(DateTime date)
    {
        _logger.LogInformation("Weather forecast for {Date} requested", date);

        var forecast = new WeatherForecast
        {
            Date = DateOnly.FromDateTime(date),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };

        return Ok(forecast);
    }
}

/// <summary>
/// Modelo de datos para el pronóstico del tiempo
/// </summary>
public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
