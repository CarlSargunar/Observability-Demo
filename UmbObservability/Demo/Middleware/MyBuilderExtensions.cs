using UmbObservability.Demo.Client;
using UmbObservability.Demo.Services;

namespace UmbObservability.Demo.Middleware;

public static class MyBuilderExtensions
{
    public static IUmbracoBuilder RegisterCustomServices(this IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton<IStartupFilter, MiddlewareStartupFilter>();
        builder.Services.AddSingleton<IEmailService, EmailService>();

        // Weather API Client url is taken from Aspire Service Discovery
        builder.Services.AddHttpClient<IWeatherApiClient, WeatherApiClient>(client =>
        {
            // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
            // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
            client.BaseAddress = new("https+http://weatherapi");
        });

        return builder;
    }

    public static IUmbracoBuilder AddCustomServices(this IUmbracoBuilder builder)
    {
        builder.RegisterCustomServices();
        return builder;
    }
}