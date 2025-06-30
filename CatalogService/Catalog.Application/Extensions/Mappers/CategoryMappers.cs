using Catalog.Application.Categories.Dtos;
using Catalog.Domain.Entities;

namespace Catalog.Application.Extensions.Mappers;

public static class CategoryMappers
{
    public static CategoryDto ToDto(this Category category)
    {
        var categoryDto = new CategoryDto(
            category.Id,
            category.Name,
            category.Image,
            category.ParentCategory?.Id,
            category.ParentCategory?.Name,
            category.ParentCategory?.Image);

        return categoryDto;
    }
}