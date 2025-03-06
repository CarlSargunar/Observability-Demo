using UmbObservability.Demo.Controllers;

namespace UmbObservability.Demo.Services;

public interface IEmailService
{
    Task<string> SendEmail(ContactFormViewModel model);
}