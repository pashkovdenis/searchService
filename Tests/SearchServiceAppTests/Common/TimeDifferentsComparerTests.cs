using System;
using SearchService.Domain.ValueObjects;
using Xunit;

namespace SearchServiceAppTests.Common;

public sealed class TimeDifferentsComparerTests
{
    [Fact]
    public void Compare_Should_Correctly_Compare_Dates()
    {
        // Arrange 
        var comparer = new DateTimeComparer.DateTimeComparer();
        var payload = new Payload
        {
            EventData = new EventPayload
            {
                Start = DateTimeOffset.Now.AddHours(12)
            }
        };

        // Act 
        var diff = comparer.Compare(null, payload);

        // Assert
        Assert.True(diff > 0);
        Assert.True(diff < 1);
    }
}