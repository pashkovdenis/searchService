using MongoDB.Bson;
using MongoDB.Driver;
using SearchService.App.Abstraction.Infrastructure;
using SearchService.Infrastructure.Repositories;
using SymbolProducer.Abstractions;
using SymbolProducer.Infrastructure;

namespace SearchServiceAPI.Extensions;

internal static class DatabaseConfigExtensions
{
    /// <summary>
    /// Add Mongodatabase settings
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddMongoDatabase(this IServiceCollection serviceCollection, IConfiguration config)
    {
        var connString = config.GetConnectionString("db");
        
        var dbName = config["dbName"];
        
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

        serviceCollection.AddTransient<IMongoDatabase>(_ => new MongoClient(connString).GetDatabase(dbName));
        serviceCollection.AddTransient<IIndexRepository, IndexMongoRepository>();
        serviceCollection.AddTransient<IDirectoryRepository, DirectoryMongoRepository>();
        serviceCollection.AddTransient<ISymbolRepository, MongoRepository>();
        
        return serviceCollection;
    }
    
}