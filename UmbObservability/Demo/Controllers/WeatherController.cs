
using System.Diagnostics;
using UmbObservability.Demo.Client;
using UmbObservability.Demo.OTel;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbObservability.Demo.Controllers;

public class WeatherController : RenderController
{
    private readonly ILogger<ContactController> _logger;
    private readonly WeatherApiClient _weatherApiClient;

    public WeatherController(ILogger<ContactController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor, ServiceContext context, WeatherApiClient weatherApiClient)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _logger = logger;
        _weatherApiClient = weatherApiClient;
    }

    public override IActionResult Index()
    {
        _logger.LogInformation("Weather page visited");

        // Call the Weather API
        var forecasts = _weatherApiClient.GetWeatherAsync().Result;
        ViewData["Forecasts"] = forecasts;


        // return our custom ViewModel
        return CurrentTemplate(CurrentPage);
    }
}