using Catalog.Application.Extensions.Mappers;
using Catalog.Application.Products.Dtos;
using Catalog.Domain.Contracts;
using Catalog.Domain.Exceptions;
using MediatR;

namespace Catalog.Application.Products.Queries;

public record GetProductQuery(Guid Id) : IRequest<ProductDto>;

public class GetProductQueryHandler(IProductRepository repository) : IRequestHandler<GetProductQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetProductWithCategoryAsync(request.Id)
                      ?? throw new NotFoundException($"The product with given id: {request.Id} does not exist");

        var dto = product.ToDto();

        return dto;
    }
}