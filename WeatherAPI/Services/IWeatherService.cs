namespace WeatherAPI.Services;

public interface IWeatherService
{
    void CheckTemperature(int maxTemp);
    void CheckTemperatureTrend(int hotDays, int coldDays);
}