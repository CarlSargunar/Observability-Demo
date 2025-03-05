var builder = DistributedApplication.CreateBuilder(args);

var weatherApi = builder.AddProject<Projects.WeatherAPI>("weatherapi");

builder.Build().Run();
