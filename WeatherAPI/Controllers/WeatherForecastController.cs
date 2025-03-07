using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Wet", "Rainy", "Drizzly", "Mizzly", "Cats and Dogs", "Spitting", "Bucketing down", "Pissing", "Smirr", "Plothering"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _configuration;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        var weatherData = LoadWeatherData();

        var forecasts = Enumerable.Range(1, 10).Select(index =>
        {
            // Hack to get some random weather
            var randomWeather = weatherData[Random.Shared.Next(weatherData.Count)];

            return new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-5, 30),
                randomWeather.Name,
                randomWeather.Description
            );
        }).ToArray();

        var hotDays = forecasts.Count(x => x.TemperatureC > 20);
        var coldDays = forecasts.Count(x => x.TemperatureC < 5);
        var maxTemp = forecasts.Max(x => x.TemperatureC);

        // Some logging
        _logger.LogInformation("Generated {0} weather reports. {1} hot days, {2} cold days.", forecasts.Length, hotDays, coldDays);

        // Do some made-up logging and exceptions for the demo
        if (hotDays > coldDays)
        {
            // Sleep for a random amount of time between 0.3 and 0.8 seconds
            var sleep = Random.Shared.Next(300, 1500);
            Thread.Sleep(sleep);
            int heatwaveDays = hotDays - coldDays;
            _logger.LogWarning("Heatwave!!! Things are slowing down: {0}", heatwaveDays);
        }

        if (maxTemp > 28)
        {
            _logger.LogCritical("Overheating!!! Temp : {0}", maxTemp);
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = $"Temperature too high: {maxTemp}C",
                Status = 500
            });
        }

        return Ok(forecasts);
    }

    [HttpPost(Name = "PostWeatherForecast")]
    public IActionResult Post([FromBody] WeatherForecast forecast)
    {
        if (forecast == null)
        {
            return BadRequest("Invalid weather forecast data.");
        }

        // Here you can add logic to save the forecast data to a database or any other storage
        _logger.LogInformation("Received weather forecast: {0}", forecast);

        return Ok("Weather forecast received successfully.");
    }

    private List<WeatherData> LoadWeatherData()
    {
        var relativePath = _configuration["WeatherDataFile"].TrimStart('/', '\\'); ;
        var filePath = Path.Combine(AppContext.BaseDirectory, relativePath);

        if (string.IsNullOrWhiteSpace(filePath) || !System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException("Weather data file not found", filePath);
        }

        var lines = System.IO.File.ReadAllLines(filePath).Skip(1); // Skip the header
        var records = lines
            .Select(line =>
            {
                var parts = line.Split(',');
                return new WeatherData(parts[0].Trim('"'), parts[1].Trim('"'));
            })
            .ToList();

        return records;
    }

}
