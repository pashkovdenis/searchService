using MongoDB.Driver;
using SearchService.App.Abstraction.Infrastructure;
using SearchService.Domain.Models;

namespace SearchService.Infrastructure.Repositories;

public sealed class DirectoryMongoRepository : IDirectoryRepository
{
    private readonly IMongoCollection<IndexDirectory> _collection;

    public DirectoryMongoRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<IndexDirectory>(nameof(IndexDirectory));
    }

    public async Task<IEnumerable<IndexDirectory>> GetAllAsync(string token) => await (await _collection.FindAsync(x => x.Token == token)).ToListAsync();

    public ValueTask InsertAsync(IndexDirectory indexDirectory) => new(_collection.InsertOneAsync(indexDirectory));
   
    public ValueTask DropAsync(IndexDirectory directory) => new(_collection.DeleteOneAsync(x => x.Id == directory.Id)); 

    public async Task<IndexDirectory> FindByIdAsync(Guid directoryId)
        => await (await _collection.FindAsync(x => x.Id == directoryId)).FirstOrDefaultAsync();
}