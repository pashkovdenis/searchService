using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchService.Domain.ValueObjects;
using SymbolProducer.Infrastructure;
using SymbolProducer.Models;
using Xunit;

namespace SymbolProducerTests;

public class ProducerTests : IClassFixture<DbFixture>
{
    private readonly DbFixture _db;

    public ProducerTests(DbFixture db)
    {
        _db = db;
    }

    [Fact]
    public async Task ShouldInsertSeveralTokens()
    {
        // Arrange 
        var producer = new SymbolProducer.SymbolProducer(new MongoRepository(_db.Database));
        var word = "Hello";

        // Act 
        var symbols = await producer.ProduceAsync(new List<string> { word }, new LayerOptions());

        // Assert
        Assert.NotEmpty(symbols);
    }

    [Fact]
    public async Task ShouldFindBySynonims()
    {
        // Arrange 
        var repository = new MongoRepository(_db.Database);
        var producer = new SymbolProducer.SymbolProducer(repository);
        var word = "Example";
        var synonim = "Sample";

        // Act 
        await repository.InsertAsync(new Word
        {
            Token = word,
            Synonims = new List<string> { synonim },
            Count = 1
        });

        var symbols = await producer.ProduceAsync(new[] { synonim }, new LayerOptions());

        // Assert
        Assert.NotEmpty(symbols);
        Assert.Equal(word, symbols.First().Token);
    }

    [Fact]
    public async Task ShouldHasDifferentWeights()
    {
        // Arrange 
        var repository = new MongoRepository(_db.Database);
        var producer = new SymbolProducer.SymbolProducer(repository);
        var word = "Server";
        var synonim = "Computer";

        // Act  
        await repository.InsertAsync(new Word
        {
            Token = word,
            Synonims = new List<string> { synonim },
            Count = 1
        });

        var foundByToken = await producer.ProduceAsync(new[] { word }, new LayerOptions());
        var foundBySynonim = await producer.ProduceAsync(new[] { synonim }, new LayerOptions());

        // Assert 
        Assert.NotEmpty(foundByToken);
        Assert.NotEmpty(foundBySynonim);
        Assert.Equal(foundBySynonim.First().Token, foundByToken.First().Token);
        Assert.True(foundByToken.First().Weight > foundBySynonim.First().Weight);
    }
}