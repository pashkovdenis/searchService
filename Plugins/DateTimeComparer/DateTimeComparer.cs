using SearchService.App.Abstraction;
using SearchService.Domain.ValueObjects;

namespace DateTimeComparer;

public sealed class DateTimeComparer : ITokenComparer
{
    public double Compare(IEnumerable<string> tokens, Payload payload)
    {
        if (payload.EventData.Start == default || DateTimeOffset.Now >= payload.EventData.Start) return 0;
        var startSeconds = payload.EventData.Start.ToUnixTimeSeconds();
        var differenceInSeconds = startSeconds - DateTimeOffset.Now.ToUnixTimeSeconds();
        var diff = 1000d - (double)differenceInSeconds / startSeconds * 1000d;
        return DistanceRegression(diff);
    }

    private static double DistanceRegression(double distance)
    {
        return CalculateWebersForce(0.01d, distance, .222d);
    }

    private static double CalculateWebersForce(double multiplier, double force, double low)
    {
        return multiplier * Math.Log10(force / low);
    }
}