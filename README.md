# Observability-Demo

A Sample app demonstrating wiring up Observability using Open Telemetry to an Umbraco app, using .NET Aspire.

## Demo Notes

Add the following to ServiceDefaults.Extensions.cs, in the ConfigureOpenTelemetry method


```csharp
builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    // Add Custom Metric - Demo:2
                    .AddMeter("UmbracoMetrics");
            })
            .WithTracing(tracing =>
            {
                tracing.AddSource(builder.Environment.ApplicationName)
                    .AddAspNetCoreInstrumentation()
                    // Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
                    //.AddGrpcClientInstrumentation()
                    .AddHttpClientInstrumentation();
            });

        builder.AddOpenTelemetryExporters();
```



## References

- Open Telemetry
    - https://opentelemetry.io/
    - https://opentelemetry.io/docs/languages/net/
    - https://github.com/open-telemetry
- .NET Aspire
    - https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview
- Aspire with Grafana and Prometheus
    - https://devblogs.microsoft.com/dotnet/introducing-aspnetcore-metrics-and-grafana-dashboards-in-dotnet-8/


## Troubleshooting

Sometimes the networking on windows gets messed up, and the app cannot bind to port 5001. To fix this, restart the **Host Network Service** on windows Services, or with the following command:

```
net stop HNS
net start HNS
```

