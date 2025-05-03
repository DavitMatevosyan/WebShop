namespace Catalog.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public bool IsDeleted { get; protected set; }
}
