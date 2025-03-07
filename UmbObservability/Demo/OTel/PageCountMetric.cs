using System.Diagnostics.Metrics;

namespace UmbObservability.Demo.OTel;

// Custom Metric to track the number of page visits : Demo:3
public static class PageCountMetric
{
    //Resource name in Open Telemetry
    public const string MetricName = "UmbracoMetrics";

    public static Meter Meter = new(MetricName);

    // Metric to track the number of page visits 
    public static Counter<int> PageCounter = Meter.CreateCounter<int>("page.count");

}