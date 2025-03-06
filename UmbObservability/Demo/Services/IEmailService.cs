using UmbObservability.Demo.ContactForm;

namespace UmbObservability.Demo.Services;

public interface IEmailService
{
    Task<string> SendEmail(MyContactFormViewModel model);
}