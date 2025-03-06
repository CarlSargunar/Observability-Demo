namespace UmbObservability.Demo.Client;

public interface IWeatherApiClient
{
    Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default);
}