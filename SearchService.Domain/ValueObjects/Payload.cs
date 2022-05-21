namespace SearchService.Domain.ValueObjects;

public sealed class Payload
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public List<Symbol> Symbols { get; init; } = new();

    public double Delta { get; set; }

    public string Data { get; set; }

    public string Type { get; set; }

    public string InternalId { get; set; }

    public EventPayload EventData { get; set; }
}