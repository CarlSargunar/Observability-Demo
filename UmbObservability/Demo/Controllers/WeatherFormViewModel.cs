using System.ComponentModel.DataAnnotations;

namespace UmbObservability.Demo.Controllers;

public class WeatherFormViewModel
{
    [Required]
    public string Weather { get; set; }
    [Required]
    public int  Temperature{ get; set; }
    public string Summary { get; set; }
}