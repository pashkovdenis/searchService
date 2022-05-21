using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using SearchService.App.Abstraction.Infrastructure;
using SearchService.App.Common;
using SearchService.App.UseCases.Reinforce;
using SearchService.Domain.Enumerations;
using SearchService.Domain.Models;
using SymbolProducer.Abstractions;
using SymbolProducer.Models;
using Xunit;

namespace SearchServiceAppTests.UseCase.Reinforce;

public sealed class ReinforceHandlerTests
{
    [Fact]
    public async Task Should_Store_Correctly_Payload()
    {
        // Arrange 
        var directoryRepositoryMock = new Mock<IDirectoryRepository>();
        var indexRepositoryMock = new Mock<IIndexRepository>();
        var symbolProducerMock = new SymbolProducer.SymbolProducer(new SymbolRepository()); 
        var comparerFactory = new ComparerFactory(new DefaultComparer());
        var directoryId = Guid.NewGuid();
        var directory = new IndexDirectory
        {
            Id = directoryId,
            Layers = new List<Layer>
            {
                new Layer
                {
                    Id = Guid.NewGuid()
                }
            },
            Mode = SearchMode.Sequental,
            Name = "default",
            Token = "default"
        };
        directoryRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(directory);
        
        var index = new SearchService.Domain.Models.Index();
        indexRepositoryMock.Setup(x => x.FindOneAsync(It.IsAny<Guid>())).ReturnsAsync(index);
        
        var handler = new ReinforceHandler(new ReinforceOutput(), directoryRepositoryMock.Object,
            indexRepositoryMock.Object, symbolProducerMock, comparerFactory);
      
        var dataSet = new List<string[]>
        {
            new[] { "1", "1", "false" }, 
            new[] { "1", "0", "true" }, 
            new[] { "0", "1", "true" }, 
            new[] { "0", "0", "false" }
        };

        var input = new ReinforceInput(directoryId, directory.Layers.First().Id,
            new List<ReinforceInput.ReinforceRecord>
            {
                new ReinforceInput.ReinforceRecord
                {
                    Context = new (),
                    IndernalId = "true",
                    IndexId = index.Id,
                    PayloadData = "true",
                    PayloadId = Guid.NewGuid(),
                    Tokens = new List<string> { "1", "0" },
                    Type = "default"
                },
                new ReinforceInput.ReinforceRecord
                {
                    Context = new (),
                    IndernalId = "true",
                    IndexId = index.Id,
                    PayloadData = "true",
                    PayloadId = Guid.NewGuid(),
                    Tokens = new List<string> { "0", "1" },
                    Type = "default"
                },
                new ReinforceInput.ReinforceRecord
                {
                    Context = new (),
                    IndernalId = "false",
                    IndexId = index.Id,
                    PayloadData = "false",
                    PayloadId = Guid.NewGuid(),
                    Tokens = new List<string> { "1", "1" },
                    Type = "default"
                },
                new ReinforceInput.ReinforceRecord
                {
                    Context = new (),
                    IndernalId = "false",
                    IndexId = index.Id,
                    PayloadData = "false",
                    PayloadId = Guid.NewGuid(),
                    Tokens = new List<string> { "0", "0" },
                    Type = "default"
                },
            }, index.Id); 

        // Act 
        await handler.Execute(input); 
        
        // Assert
        Assert.NotEmpty(index.Payloads);

        foreach (var set in dataSet)
        {
            var inp = set[..2];
            var result = Algorithms.CalculatePayloadScore(inp, index.Payloads, comparerFactory.GetComparer(string.Empty));
            var top = result.First();
            var payload = index.Payloads.First(x => x.Id == top.payloadId);
            Assert.NotNull(payload); 
            Assert.Equal(set[2], payload.InternalId);
        } 
    }
    
    public sealed class SymbolRepository : ISymbolRepository
    {
        private readonly List<Word> _words = new();
        public Task<Word> FindAsync(string v) 
            => Task.FromResult<Word>(_words.FirstOrDefault(x => x.Token == v || x.Synonims.Contains(v)));

        public Task InsertAsync(Word foundToken)
        {
            _words.Add(foundToken);
            return Task.CompletedTask;
        }
        
        public Task UpdateAsync(Word foundToken) => Task.CompletedTask;
    }
    
    public sealed class ReinforceOutput : IReinforceOutput
    {
        public string ErrorMessage { get; set; }
        
        public void Error(string message)
        {
            ErrorMessage = message;
        }
    }
}