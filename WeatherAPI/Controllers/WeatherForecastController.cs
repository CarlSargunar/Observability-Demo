using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

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
    private readonly IWeatherService _weatherService;
    private readonly IConfiguration _configuration;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService, IConfiguration configuration)
    {
        _logger = logger;
        _weatherService = weatherService;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var range = Random.Shared.Next(5, 30);

        var weatherData = LoadWeatherData();

        var forecasts = Enumerable.Range(1, range).Select(index =>
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
        var minTemp = forecasts.Min(x => x.TemperatureC);

        // Some logging
        _logger.LogInformation("Generated {0} weather reports. {1} hot days, {2} cold days.", forecasts.Length, hotDays, coldDays);

        // Calling another service
        _weatherService.CheckTemperature(maxTemp);
        _weatherService.CheckTemperatureTrend(hotDays, coldDays);

        return forecasts;
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
