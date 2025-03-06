using UmbObservability.Demo.Services;

namespace UmbObservability.Demo.Middleware;

public static class MyBuilderExtensions
{
    public static IUmbracoBuilder RegisterCustomServices(this IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton<IStartupFilter, MiddlewareStartupFilter>();
        builder.Services.AddSingleton<IEmailService, EmailService>();
        return builder;
    }

    public static IUmbracoBuilder AddCustomServices(this IUmbracoBuilder builder)
    {
        builder.RegisterCustomServices();
        return builder;
    }
}