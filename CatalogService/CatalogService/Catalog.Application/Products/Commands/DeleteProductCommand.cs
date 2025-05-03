using Catalog.Domain.Contracts;
using Catalog.Domain.Exceptions;
using MediatR;

namespace Catalog.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<Guid>;

public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, Guid>
{
    public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(request.Id)
            ?? throw new NotFoundException("The product does not exist");

        product.DeleteProduct();

        await productRepository.UpdateAsync(product);
        
        return product.Id;
    }
}