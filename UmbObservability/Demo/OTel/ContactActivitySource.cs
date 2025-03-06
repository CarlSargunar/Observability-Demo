using System.Diagnostics;
namespace UmbObservability.Demo.OTel;

public static class ContactActivitySource
{
    public static readonly ActivitySource ActivitySource = new ActivitySource("UmbObservability.ContactForm");
}