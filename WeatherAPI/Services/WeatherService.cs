namespace WeatherAPI.Services;

public class WeatherService : IWeatherService
{
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(ILogger<WeatherService> logger)
    {
        _logger = logger;
    }


    public void ReactToTemperature(int maxTemp)
    {
        if (maxTemp > 28)
        {
            _logger.LogCritical("Overheating!!! Temp : {0}", maxTemp);
            throw new Exception("Something has gone wrong - this is an artificial error");
        }
    }

    public void ReactToTrend(int hotDays, int coldDays)
    {
        if (hotDays > coldDays)
        {
            // Sleep for a random amount of time between 0.3 and 0.8 seconds
            var sleep = Random.Shared.Next(300, 800);
            Thread.Sleep(sleep);
            int heatwaveDays = hotDays - coldDays;
            _logger.LogWarning("Heatwave!!! Things are slowing down: {0}", heatwaveDays);
        }
    }
}