using System.ComponentModel.DataAnnotations;

namespace UmbObservability.Demo.ContactForm;

public class MyContactFormViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Message { get; set; }
    public string? Subject { get; set; }
}

