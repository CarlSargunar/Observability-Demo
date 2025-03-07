using UmbObservability.Demo.Client;
using UmbObservability.Demo.OTel;
using UmbObservability.Demo.Services;

namespace UmbObservability.Demo.Controllers;

public class WeatherFormSurfaceController : SurfaceController
{
    private readonly IEmailSender _emailSender;
    private readonly IOptions<GlobalSettings> _globalSettings;
    private readonly ILogger<WeatherFormSurfaceController> _logger;
    private readonly IWeatherApiClient _weatherApiClient;

    public WeatherFormSurfaceController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        IEmailSender emailSender,
        IOptions<GlobalSettings> globalSettings, 
        ILogger<WeatherFormSurfaceController> logger,
        IWeatherApiClient weatherApiClient)
        : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _emailSender = emailSender;
        _globalSettings = globalSettings;
        _logger = logger;
        _weatherApiClient = weatherApiClient;
    }

    public async Task<IActionResult> Submit(WeatherFormViewModel model)
    {
        using var activity = ContactActivitySource.ActivitySource.StartActivity("SubmitWeatherForm");

        if (!ModelState.IsValid)
        {
            TempData["Message"] = "Please fill in all required fields";
            return CurrentUmbracoPage();
        }

        // Nothing really happening here, just a placeholder for the demo
        await _weatherApiClient.PostWeatherAsync(model);

        TempData["Message"] = "Weather Submitted!";

        return RedirectToCurrentUmbracoPage();
    }
}