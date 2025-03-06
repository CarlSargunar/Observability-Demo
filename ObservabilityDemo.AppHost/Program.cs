var builder = DistributedApplication.CreateBuilder(args);

var weatherApi = builder.AddProject<Projects.WeatherAPI>("weatherapi");

var website = builder.AddProject<Projects.UmbObservability>("website")
    .WithReference(weatherApi)
    .WaitFor(weatherApi);
    
builder.Build().Run();
