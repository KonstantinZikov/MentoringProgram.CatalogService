using Application.Common.Identity;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Identity;
using Domain.ValueObjects;
using MediatR;

namespace Application.Categories.Commands;

[Authorize(Roles.Manager)]
public record CreateCategoryCommand : IRequest<int>
{
    public required string Name { get; init; }

    public string? ImageUrl { get; init; }

    public int? ParentCategoryId { get; init; }
}

public class CreateCategoryCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        { 
            Name = request.Name,
            Image = request.ImageUrl != null? new Image { Url = request.ImageUrl } : null,
            
        };

        if (request.ParentCategoryId != null)
        {
            Category? parentCategory = await context.Categories
                .FindAsync([request.ParentCategoryId], cancellationToken);

            Guard.Against.NotFound((int)request.ParentCategoryId, parentCategory);

            category.ParentCategory = parentCategory;
        }

        if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            category.Image = new Image { Url = request.ImageUrl };
        }

        context.Categories.Add(category);

        await context.SaveChangesAsync(cancellationToken);
        return category.Id;
    }
}
