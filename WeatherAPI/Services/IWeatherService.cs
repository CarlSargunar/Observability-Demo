namespace WeatherAPI.Services;

public interface IWeatherService
{
    void ReactToTemperature(int maxTemp);
    void ReactToTrend(int hotDays, int coldDays);
}