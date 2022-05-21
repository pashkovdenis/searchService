using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

Console.WriteLine("Dictionary importer");
Console.WriteLine("Import dictionary with synonims into the mongo db");

// config values.
var configName = "appsettings.json";
var dictionaryFileName = "words.csv";  

// Get database for the tokens
var database = GetDatabase(configName);

var collection = database.GetCollection<Word>(nameof(Word)); 

var totalLines = 0;
var insertedLines = 0;
var lines = File.ReadAllLines(dictionaryFileName);

IProgress<int> progress = new Progress<int>(x => Console.WriteLine( $"Progress {((double)x/totalLines * 100)}%"));

totalLines = lines.Length - 1;

await Parallel.ForEachAsync(lines, async (line, ct) =>
{
    await ProceedLine(line);
    progress.Report(++insertedLines);
});

Console.WriteLine("End parsing");
Console.ReadLine(); 

// End of the importing logic 

async Task ProceedLine(string line)
{
    if (string.IsNullOrWhiteSpace(line) || !line.Contains(','))
    {
        return;
    }
    
    var tokens = line.Split(',');

    var foundWord = (await collection.FindAsync(x => x.Token == tokens[0])).FirstOrDefault();

    if (foundWord == null)
    {
        foundWord = new Word
        {
            Token = tokens[0], 
            Count = 1, 
            Id = Guid.NewGuid(), 
            Synonims = new List<string>()
        }; 
    }

    var synonims = tokens[1..].Where(x => !foundWord.Synonims.Contains(x));

    foreach (var s in synonims)
    {
        foundWord.Synonims.Add(s);
    }

    // Update 
    await collection.ReplaceOneAsync(Builders<Word>.Filter.Eq(e => e.Id, foundWord.Id), foundWord,  new ReplaceOptions { IsUpsert = true }); 
}

 

static IMongoDatabase GetDatabase(string configFileName)
{
    var config = new ConfigurationBuilder()
        .AddJsonFile(configFileName)
        .Build();
    
    var connString = config.GetConnectionString("db");
    var dbName = config["dbName"];
    var DbContextSettings = new MongoDbContextSettings(connString, dbName);

    return new MongoClient(DbContextSettings.ConnectionString).GetDatabase(DbContextSettings.DatabaseName);
}

internal class MongoDbContextSettings
{
    public MongoDbContextSettings(string connectionString, string databaseName)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
    }
    
    public string ConnectionString { get; }
    public string DatabaseName { get; }
}

public class Word
{
    public string Token { get; init; }
    public ICollection<string> Synonims { get; set; }
    public int Count { get; set; }
    public Guid Id { get; init; } = Guid.NewGuid();
}

