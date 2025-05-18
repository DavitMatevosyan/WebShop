using Catalog.Domain.Contracts;
using Catalog.Domain.Exceptions;
using MediatR;

namespace Catalog.Application.Products.Commands;

public record UpdateProductCommand (
    Guid Id,
    string? Name = null, 
    string? Description = null,
    string? Image = null,
    Guid? CategoryId = null,
    decimal? Price = null,
    int? Amount = null
    ) : IRequest<Guid>;

public class UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : IRequestHandler<UpdateProductCommand, Guid>
{
    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken) // cancellation token not used currently, but can be used.
    {
        var existingProduct = await productRepository.GetAsync(request.Id) 
                       ?? throw new NotFoundException("Product not found");
    
        if(request.Name is not null)
            existingProduct.ChangeName(request.Name);

        if(request.Description is not null)
            existingProduct.ChangeDescription(request.Description);

        if(request.Image is not null)
            existingProduct.ChangeImage(request.Image);

        if (request.CategoryId is not null)
        {
            var category = await categoryRepository.GetAsync(request.CategoryId.Value)
                           ?? throw new NotFoundException("Category not found");

            existingProduct.ChangeCategoryId(category.Id);
        }

        if (request.Price is not null)
            existingProduct.ChangePrice(request.Price.Value);
        
        if(request.Amount is not null)
            existingProduct.ChangeAmount(request.Amount.Value);
        
        await  productRepository.SaveChangesAsync();
        return existingProduct.Id;
    }
}