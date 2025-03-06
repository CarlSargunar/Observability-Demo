using UmbObservability.Demo.Controllers;

namespace UmbObservability.Demo.Client;

public class WeatherApiClient : IWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiClient> _logger;

    public WeatherApiClient(HttpClient httpClient, ILogger<WeatherApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<WeatherForecast>? forecasts = null;

        var forecastData = _httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken);

        await foreach (var forecast in forecastData.WithCancellation(cancellationToken))
        {
            if (forecasts?.Count >= maxItems)
            {
                break;
            }
            if (forecast is not null)
            {
                forecasts ??= [];
                forecasts.Add(forecast);
            }
        }

        _logger.LogInformation("Received {0} weather forecasts.", forecasts?.Count ?? 0);

        return forecasts?.ToArray() ?? [];
    }

    public async Task PostWeatherAsync(WeatherFormViewModel model)
    {
        var weatherForecast = new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now),
            model.Temperature,
            model.Weather,
            model.Summary
        );

        _logger.LogInformation("Posting weather forecast: {0}", weatherForecast);

        var response = await _httpClient.PostAsJsonAsync("/weatherforecast", weatherForecast);

        response.EnsureSuccessStatusCode();
    }
}