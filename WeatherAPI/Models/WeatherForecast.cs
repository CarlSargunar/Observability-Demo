namespace WeatherAPI.Models;

public record WeatherForecast(DateOnly ForecastDate, int TemperatureC, string? Name, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}