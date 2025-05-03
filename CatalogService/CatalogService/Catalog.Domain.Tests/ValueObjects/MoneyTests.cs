using Catalog.Domain.Exceptions;
using Catalog.Domain.ValueObjects;
using FluentAssertions;

namespace Catalog.Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Constructor_PositiveValue_ShouldCreate()
    {
        Money money = new Money(50);
        
        Assert.Equal(50, money.Value);
    }
    
    [Fact]
    public void Constructor_NegativeValue_ShouldCreate()
    {
        var act = () => new Money(-50);

        act.Should().Throw<DomainException>();
    }
}