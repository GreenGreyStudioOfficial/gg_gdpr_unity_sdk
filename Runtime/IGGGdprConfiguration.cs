namespace GreenGrey.GDPR
{
    public interface IGGGdprConfiguration
    {
        string serviceUrl { get; }
        int requestTimeout { get; }
        int requestAttempts { get; }
        bool withLogs { get; }
    }
}