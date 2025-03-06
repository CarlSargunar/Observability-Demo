using System.ComponentModel.DataAnnotations;

namespace UmbObservability.Demo.Controllers;

public class WeatherFormViewModel
{
    [Required]
    public string Weather { get; set; }
    [Required]
    public string Location { get; set; }
    public string Description { get; set; }
}