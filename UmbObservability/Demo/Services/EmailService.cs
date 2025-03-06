using UmbObservability.Demo.ContactForm;
using UmbObservability.Demo.OTel;

namespace UmbObservability.Demo.Services;

public class EmailService : IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailService> _logger;
    private readonly IOptions<GlobalSettings> _globalSettings;

    public EmailService(
        IEmailSender emailSender,
        ILogger<EmailService> logger,
        IOptions<GlobalSettings> globalSettings)
    {
        _emailSender = emailSender;
        _logger = logger;
        _globalSettings = globalSettings;
    }

    public async Task<string> SendEmail(MyContactFormViewModel model)
    {
        using var activity = ContactActivitySource.ActivitySource.StartActivity("SendEmail");
        activity?.SetTag("service", nameof(SendEmail));
        activity?.SetTag("email.to", model.Email);
        activity?.SetTag("email.subject", model.Subject);

        try
        {
            var fromAddress = _globalSettings.Value?.Smtp?.From ?? "noreply@umbraco.com";
            var subject = model.Subject ?? $"Website Contact form: {model.Name}";

            var mailMessage = new EmailMessage(
                fromAddress,
                model.Email,
                subject,
                model.Message,
                isBodyHtml: true
            );

            await _emailSender.SendAsync(mailMessage, "StarterKitContactEmail");

            return "Message submitted";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while submitting the form");
            return "An error occurred while submitting the form";
        }
    }
}

