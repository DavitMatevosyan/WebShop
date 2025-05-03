using Catalog.Application.Products.Dtos;
using Catalog.Domain.Entities;

namespace Catalog.Application.Extensions.Mappers;

public static class ProductMappers
{
    public static ProductDto ToDto(this Product product)
    {
        var categoryDto = product.Category.ToDto();
    
        var productDto = new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Image,
            categoryDto,
            product.Price,
            product.Amount);

        return productDto;
    }
}