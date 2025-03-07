using UmbObservability.Demo.OTel;

namespace UmbObservability.Demo.Middleware;

public class MyPageCountMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MyPageCountMiddleware> _logger;

    public MyPageCountMiddleware(RequestDelegate next, ILogger<MyPageCountMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request is for static assets (CSS/JS/images)
        if ((context.Request.Method == HttpMethods.Get || context.Request.Method == HttpMethods.Post) && !IsStaticAsset(context.Request.Path))
        {
            _logger.LogInformation($"Request for {context.Request.Path} received");
            var urlName = context.Request.Path.Value.ToLowerInvariant();
            // Increment metrics, and pass additional information in context

            // Demo: 4 - Increment the page count metric

            PageCountMetric.PageCounter.Add(1,
                new KeyValuePair<string, object>("page.url", urlName),
                new KeyValuePair<string, object>("page.method]", context.Request.Method),
                new KeyValuePair<string, object>("page.user_agent", context.Request.Headers["User-Agent"].ToString())); 
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }

    private bool IsStaticAsset(string path)
    {
        // Regex pattern to match typical static asset extensions
        var staticAssetPattern = @"\.(css|js|png|jpg|jpeg|gif|svg|ico)$";
        return Regex.IsMatch(path, staticAssetPattern, RegexOptions.IgnoreCase);
    }
}
