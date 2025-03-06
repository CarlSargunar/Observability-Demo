using UmbObservability.Demo.OTel;
using UmbObservability.Demo.Services;

namespace UmbObservability.Demo.ContactForm;

public class MyContactFormSurfaceController : SurfaceController
{
    private readonly IEmailSender _emailSender;
    private readonly IOptions<GlobalSettings> _globalSettings;
    private readonly ILogger<MyContactFormSurfaceController> _logger;
    private readonly IEmailService _emailService;

    public MyContactFormSurfaceController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        IEmailSender emailSender,
        IOptions<GlobalSettings> globalSettings, 
        ILogger<MyContactFormSurfaceController> logger,
        IEmailService emailService)
        : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _emailSender = emailSender;
        _globalSettings = globalSettings;
        _logger = logger;
        _emailService = emailService;
    }

    public async Task<IActionResult> Submit(MyContactFormViewModel model)
    {
        using var activity = ContactActivitySource.ActivitySource.StartActivity("SubmitContactForm");
        activity?.SetTag("controller", nameof(Submit));
        activity?.SetTag("form.name", model.Name);
        activity?.SetTag("form.email", model.Email);

        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        TempData["Message"] = await _emailService.SendEmail(model);

        return RedirectToCurrentUmbracoPage();
    }
}