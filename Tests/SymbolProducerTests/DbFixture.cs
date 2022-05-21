using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SymbolProducerTests;

public class DbFixture
{
    public DbFixture()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connString = config.GetConnectionString("db");

        var dbName = $"test_db_{Guid.NewGuid()}";

        DbContextSettings = new MongoDbContextSettings(connString, dbName);

        Database = new MongoClient(DbContextSettings.ConnectionString)
            .GetDatabase(DbContextSettings.DatabaseName);
    }


    public MongoDbContextSettings DbContextSettings { get; }

    public IMongoDatabase Database { get; }


    public void Dispose()
    {
        var client = new MongoClient(DbContextSettings.ConnectionString);
        client.DropDatabase(DbContextSettings.DatabaseName);
    }
}

public class MongoDbContextSettings
{
    public MongoDbContextSettings(string connectionString, string databaseName)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
    }

    public string ConnectionString { get; }
    public string DatabaseName { get; }
}