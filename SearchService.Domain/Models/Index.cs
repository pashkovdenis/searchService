using SearchService.Domain.Abstractions;
using SearchService.Domain.ValueObjects;

namespace SearchService.Domain.Models;

/// <summary>
///     Single index
/// </summary>
public sealed class Index : IEntity
{
    public Guid LayerId { get; init; }

    public HashSet<Payload> Payloads { get; init; } = new();

    public Guid Id { get; init; } = Guid.NewGuid();
    
    public Guid DirectoryId { get; init; }
}