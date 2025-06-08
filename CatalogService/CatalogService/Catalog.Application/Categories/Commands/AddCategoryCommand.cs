using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using MediatR;

namespace Catalog.Application.Categories.Commands;

public record AddCategoryCommand(string Name, string? Image, Guid? ParentCategoryId) : IRequest<Guid>;

public class AddCategoryCommandHandler(ICategoryRepository repository) : IRequestHandler<AddCategoryCommand, Guid>
{
    public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken) // cancellation token not used currently, but can be used.
    {
        if (request.ParentCategoryId != null)
        {
            var parentCategory = await repository.GetAsync(request.ParentCategoryId.Value);

            if (parentCategory == default)
                throw new NotFoundException("Provided parent category does not exist");
        }
        
        var category = new Category(request.Name, request.Image, request.ParentCategoryId);

        await repository.AddAsync(category);

        await repository.SaveChangesAsync();

        return category.Id;
    }
}