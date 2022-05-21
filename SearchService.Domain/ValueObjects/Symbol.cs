namespace SearchService.Domain.ValueObjects;

public sealed class Symbol
{
    public string Token { get; init; } = string.Empty;

    public double Weight { get; set; }

    public override string ToString()
    {
        return $"{Token} : {Weight}";
    }
}