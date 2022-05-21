using SearchService.Domain.Abstractions;

namespace SymbolProducer.Models;

public sealed class Word : IEntity
{
    public string Token { get; init; }

    public ICollection<string> Synonims { get; set; }

    public int Count { get; set; }

    public Guid Id { get; init; } = Guid.NewGuid();
}