using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Amount).IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.Property(x => x.Price)
            .HasConversion<decimal>(x => x.Value, x => new Money(x));

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products);
    }
}