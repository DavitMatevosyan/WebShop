using Catalog.Domain.Entities;

namespace Catalog.Domain.Contracts;

internal interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    T Get(Guid id);
    ICollection<T> GetItems(Func<T, bool> predicate);
    void Update(T entity);
    void Delete(Guid id);
}
