namespace SearchService.Domain.ValueObjects;

public sealed class LayerOptions
{
    public LayerOptions()
    {
    }

    public double DefaultDominance { get; init; } = .3d;

    public double Reinforce { get; init; } = .11d;

    public double ResultThresshold { get; init; } = .55d;

    public int DefaultContextSize { get; init; } = 10;


    public override string ToString()
    {
        return $"{DefaultDominance} - {Reinforce} - {ResultThresshold}";
    }
}