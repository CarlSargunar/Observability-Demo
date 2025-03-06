# Observability-Demo





## References

- Open Telemetry
    - https://opentelemetry.io/
- Open Telemetry .NET
    - https://opentelemetry.io/docs/languages/net/
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

