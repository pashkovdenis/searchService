using SearchService.App.Abstraction;
using SearchService.Domain.ValueObjects;
using SymbolProducer.Abstractions;
using SymbolProducer.Models;

namespace SymbolProducer;

public class SymbolProducer : ISymbolProducer
{
    private readonly ISymbolRepository _symbolRepository;

    public SymbolProducer(ISymbolRepository symbolRepository)
    {
        _symbolRepository = symbolRepository;
    }
 
    public async ValueTask<IEnumerable<Symbol>> ProduceAsync(IEnumerable<string> tokens, LayerOptions options)
    {
        var result = new List<Symbol>(tokens.Count());

        foreach (var inToken in tokens)
        {
            var foundToken = await _symbolRepository.FindAsync(inToken);

            if (foundToken == null)
            {
                foundToken = new Word
                {
                    Token = inToken,
                    Synonims = new List<string>()
                };
                
                await _symbolRepository.InsertAsync(foundToken);
            }

            var weight = options.DefaultDominance + (foundToken.Count + 1) * (options.DefaultDominance * options.Reinforce) / 100;
 
            if (!foundToken.Token.Equals(inToken, StringComparison.InvariantCultureIgnoreCase))
            {
                weight *= options.Reinforce;
            }

            result.Add(new Symbol
            {
                Token = foundToken.Token,
                Weight = weight
            });
            
            foundToken.Count++;
            
            await _symbolRepository.UpdateAsync(foundToken);
        }

        return result;
    }
}