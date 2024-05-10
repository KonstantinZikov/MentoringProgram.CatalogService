using Application.Common.Interface;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Carts.Commands;

public record UpdateCategoryCommand : IRequest
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? ImageUrl { get; init; }

    public int? ParentCategoryId { get; init; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _context.Categories
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.Null(category);

        if (category != null)
        {
            category.Name = request.Name;

            category.Image = request.ImageUrl != null 
                ? new Image { Url = request.ImageUrl } 
                : null;

            if (request.ParentCategoryId == null)
            {
                category.ParentCategory = null;
            }
            else
            {
                Category? parentCategory = await _context.Categories
                .FindAsync([request.ParentCategoryId], cancellationToken);

                Guard.Against.NotFound((int)request.ParentCategoryId, parentCategory);

                category.ParentCategory = parentCategory;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }       
    }
}
