using SearchService.Domain.Abstractions;
using SearchService.Domain.Enumerations;

namespace SearchService.Domain.Models;

public sealed class IndexDirectory : IEntity
{
    public SearchMode Mode { get; init; } = SearchMode.Sequental;

    public List<Layer> Layers { get; init; } = new();

    public string Token { get; set; }
    public string Name { get; set; }
    public Guid Id { get; init; } = Guid.NewGuid();
}