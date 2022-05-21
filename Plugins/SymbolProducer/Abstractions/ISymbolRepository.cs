using SymbolProducer.Models;

namespace SymbolProducer.Abstractions;

public interface ISymbolRepository
{
    Task<Word> FindAsync(string v);
    Task InsertAsync(Word foundToken);
    Task UpdateAsync(Word foundToken);
}