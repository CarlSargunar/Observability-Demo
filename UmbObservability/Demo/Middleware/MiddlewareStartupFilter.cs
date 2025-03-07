namespace UmbObservability.Demo.Middleware;

public class MiddlewareStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
    {
        app.UseMiddleware<MyPageCountMiddleware>();
        next(app);
    };
}