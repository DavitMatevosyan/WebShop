using Catalog.Domain.Contracts;
using Catalog.Domain.Exceptions;
using MediatR;

namespace Catalog.Application.Categories.Commands;

public record UpdateCategoryCommand (Guid Id, string? Name, string? Image, Guid? ParentCategoryId) : IRequest<Guid>;

public class UpdateCategoryCommandHandler(ICategoryRepository repository) : IRequestHandler<UpdateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await repository.GetAsync(request.Id) 
                       ?? throw new NotFoundException("Category not found");
    
        if(request.Name is not null)
            existingCategory.ChangeName(request.Name);

        if(request.Image is not null)
            existingCategory.ChangeImage(request.Image);

        if (request.ParentCategoryId is not null)
        {
            var category = await repository.GetAsync(request.ParentCategoryId.Value)
                           ?? throw new NotFoundException("Category not found");

            existingCategory.ChangeParentCategoryId(category.Id);
        }
        
        return existingCategory.Id;
    }
}