using ObservabilityDemo.AppHost.OpenTelemetryCollector;

var builder = DistributedApplication.CreateBuilder(args);

//var prometheus = builder.AddContainer("prometheus", "prom/prometheus")
//    .WithBindMount("../prometheus", "/etc/prometheus", isReadOnly: true)
//    .WithArgs("--web.enable-otlp-receiver", "--config.file=/etc/prometheus/prometheus.yml")
//    .WithHttpEndpoint(targetPort: 9090, name: "http");

//var grafana = builder.AddContainer("grafana", "grafana/grafana")
//    .WithBindMount("../grafana/config", "/etc/grafana", isReadOnly: true)
//    .WithBindMount("../grafana/dashboards", "/var/lib/grafana/dashboards", isReadOnly: true)
//    .WithEnvironment("PROMETHEUS_ENDPOINT", prometheus.GetEndpoint("http"))
//    .WithHttpEndpoint(targetPort: 3000, name: "http");

//builder.AddOpenTelemetryCollector("otelcollector", "../otelcollector/config.yaml")
//    .WithEnvironment("PROMETHEUS_ENDPOINT", $"{prometheus.GetEndpoint("http")}/api/v1/otlp");

//var weatherApi = builder.AddProject<Projects.WeatherAPI>("weatherapi")
//    .WaitFor(prometheus)
//    .WaitFor(grafana);

var weatherApi = builder.AddProject<Projects.WeatherAPI>("weatherapi");


builder.AddProject<Projects.UmbObservability>("website")
    .WithReference(weatherApi)
    .WaitFor(weatherApi);
    
builder.Build().Run();
