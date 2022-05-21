using SearchService.Domain.Abstractions;
using SearchService.Domain.ValueObjects;

namespace SearchService.Domain.Models;

public sealed class Layer : IEntity
{
    public LayerOptions Options { get; init; } = new();

    public Guid Id { get; set; } = Guid.NewGuid();
}