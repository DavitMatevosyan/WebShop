using Catalog.Application.Categories.Dtos;
using Catalog.Application.Extensions.Mappers;
using Catalog.Domain.Contracts;
using Catalog.Domain.Exceptions;
using MediatR;

namespace Catalog.Application.Categories.Queries;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryDto>;

public class GetCategoryQueryHandler(ICategoryRepository repository) : IRequestHandler<GetCategoryQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetAsync(request.Id)
                      ?? throw new NotFoundException($"The Category with given id: {request.Id} does not exist");

        var dto = category.ToDto();

        return dto;
    }
}