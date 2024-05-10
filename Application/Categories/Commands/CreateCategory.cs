using Application.Common.Interface;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Carts.Commands;

public record CreateCategoryCommand : IRequest<int>
{
    public required string Name { get; init; }

    public string? ImageUrl { get; init; }

    public int? ParentCategoryId { get; init; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        { 
            Name = request.Name,
            Image = request.ImageUrl != null? new Image { Url = request.ImageUrl } : null,
            
        };

        if (request.ParentCategoryId != null)
        {
            Category? parentCategory = await _context.Categories
                .FindAsync([request.ParentCategoryId], cancellationToken);

            Guard.Against.NotFound((int)request.ParentCategoryId, parentCategory);

            category.ParentCategory = parentCategory;
        }

        if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            category.Image = new Image { Url = request.ImageUrl };
        }

        _context.Categories.Add(category);

        await _context.SaveChangesAsync(cancellationToken);
        return category.Id;
    }
}
