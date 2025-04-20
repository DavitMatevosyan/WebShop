using Catalog.Domain.Entities;

namespace Catalog.Domain.Contracts;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product> GetProductWithCategoryAsync(Guid id);
}