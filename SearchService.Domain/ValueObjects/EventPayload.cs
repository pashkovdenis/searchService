namespace SearchService.Domain.ValueObjects;

public sealed class EventPayload
{
    public DateTimeOffset Start { get; init; }

    public DateTimeOffset End { get; init; }

    public TimeSpan RepeatEvent { get; init; }
}