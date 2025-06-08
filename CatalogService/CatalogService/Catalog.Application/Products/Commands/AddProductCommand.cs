using Catalog.Application.Exceptions;
using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using MediatR;

namespace Catalog.Application.Products.Commands;

public record AddProductCommand(
    string Name,
    string Description,
    string Image,
    Guid CategoryId,
    decimal Price,
    int Amount
    ) : IRequest<Guid>;

public class AddProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : IRequestHandler<AddProductCommand, Guid>
{
    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken) // cancellation token not used currently, but can be used.
    {
        var category = await categoryRepository.GetAsync(request.CategoryId)
                       ?? throw new NotFoundException("Category not found");

        var price = new Money(request.Price);

        int x = 12;

        var product = new Product(
            request.Name,
            request.Description,
            request.Image,
            category.Id,
            price,
            request.Amount);

        await productRepository.AddAsync(product);

        await productRepository.SaveChangesAsync();
        if (x == 10)
        {

        }
        return product.Id;
    }
}