using MongoDB.Driver;
using SearchService.App.Abstraction.Infrastructure;
using Index = SearchService.Domain.Models.Index;

namespace SearchService.Infrastructure.Repositories;

public sealed class IndexMongoRepository : IIndexRepository
{
    private readonly IMongoCollection<Index> _collection;

    public IndexMongoRepository(IMongoDatabase database) => _collection = database.GetCollection<Index>(nameof(Index));
    
    public ValueTask DropAllByDirectoryId(Guid id) => new(_collection.DeleteManyAsync(x => x.DirectoryId == id));
    
    public async Task<Index> FindOneAsync(Guid indexId) => 
        await (await _collection.FindAsync(x => x.Id == indexId)).FirstOrDefaultAsync();

    public Task DropSingleAsync(Guid indexId) => _collection.DeleteOneAsync(x => x.Id == indexId);

    public Task InsertAsync(Index index) => _collection.InsertOneAsync(index);

    public Task UpdateAsync(Index index) 
        => _collection.ReplaceOneAsync(Builders<Index>.Filter.Eq(e => e.Id, index.Id), index, new ReplaceOptions { IsUpsert = true });

    public async Task<IEnumerable<Index>> FindManyAsync(Guid layerId, IEnumerable<string> tokens)
    { 
        var found = await _collection.FindAsync(x => x.LayerId == layerId && x.Payloads.Any(x=> x.Symbols.Any(s=> tokens.Contains(s.Token)) ));

        return await found.ToListAsync();
    }
}