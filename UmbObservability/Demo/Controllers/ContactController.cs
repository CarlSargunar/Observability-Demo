
using System.Diagnostics;
using UmbObservability.Demo.OTel;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbObservability.Demo.Controllers;

public class ContactController : RenderController
{
    private readonly ILogger<ContactController> _logger;
    public ContactController(ILogger<ContactController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor, ServiceContext context)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _logger = logger;
    }

    public override IActionResult Index()
    {
        _logger.LogInformation("Contact page visited");

        // return our custom ViewModel
        return CurrentTemplate(CurrentPage);
    }
}