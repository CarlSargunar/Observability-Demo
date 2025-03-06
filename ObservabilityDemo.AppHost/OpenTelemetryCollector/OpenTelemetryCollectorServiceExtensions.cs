using Aspire.Hosting.Lifecycle;

namespace ObservabilityDemo.AppHost.OpenTelemetryCollector;

internal static class OpenTelemetryCollectorServiceExtensions
{
    public static IDistributedApplicationBuilder AddOpenTelemetryCollectorInfrastructure(this IDistributedApplicationBuilder builder)
    {
        builder.Services.TryAddLifecycleHook<OpenTelemetryCollectorLifecycleHook>();

        return builder;
    }
}
