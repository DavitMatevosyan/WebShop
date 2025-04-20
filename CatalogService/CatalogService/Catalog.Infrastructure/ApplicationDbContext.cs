using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure;

public class ApplicationDbContext(IDomainEventDispatcher domainEventDispatcher) : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        await domainEventDispatcher.DispatchEventAsync(domainEntities);
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}