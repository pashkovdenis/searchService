using MongoDB.Driver;
using SymbolProducer.Abstractions;
using SymbolProducer.Models;

namespace SymbolProducer.Infrastructure;

public class MongoRepository : ISymbolRepository
{
    private readonly IMongoCollection<Word> _collection;

    public MongoRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Word>(nameof(Word));
    }

    public async Task<Word> FindAsync(string word)
    {
        var found = await _collection.FindAsync(x => x.Token == word || x.Synonims.Contains(word));

        return await found.FirstOrDefaultAsync();
    }

    public Task InsertAsync(Word word)
    {
        return _collection.InsertOneAsync(word);
    }

    public Task UpdateAsync(Word foundToken)
    {
        return _collection.ReplaceOneAsync(Builders<Word>.Filter.Eq(e => e.Id, foundToken.Id), foundToken, new ReplaceOptions { IsUpsert = true });
    }
}