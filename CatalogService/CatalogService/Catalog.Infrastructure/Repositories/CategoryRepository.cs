using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext dbContext) : BaseRepository<Category>(dbContext), ICategoryRepository;