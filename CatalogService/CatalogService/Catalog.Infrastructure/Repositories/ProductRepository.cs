using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : BaseRepository<Product>(dbContext), IProductRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Product> GetProductWithCategoryAsync(Guid id)
    {
        var product = await _dbContext.Products.Where(pr => pr.Id == id)
            .Include(pr => pr.Category)
            .FirstOrDefaultAsync();
        
        return product;
    }
}