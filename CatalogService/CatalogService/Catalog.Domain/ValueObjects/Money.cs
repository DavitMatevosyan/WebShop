using Catalog.Domain.Exceptions;

namespace Catalog.Domain.ValueObjects;

public class Money
{
    public decimal Value { get; }
    
    public Money(decimal value)
    {
        if (value < 0)
            throw new DomainException("Money value cannot be negative.");

        Value = value;
    }
}