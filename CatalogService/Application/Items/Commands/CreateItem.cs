using Application.Common.Identity;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Identity;
using Domain.ValueObjects;
using MediatR;

namespace Application.Items.Commands;

[Authorize(Roles.Manager)]
public record CreateItemCommand : IRequest<int>
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public string? ImageUrl { get; init; }

    public required int CategoryId { get; init; }

    public required int Price { get; init; }

    public required string PriceCurrency { get; init; }

    public required int Amount { get; set; }
}

public class CreateItemCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateItemCommand, int>
{
    public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        Category? category = context.Categories.FindAsync([request.CategoryId], cancellationToken).Result;

        Guard.Against.NotFound(request.CategoryId, category);

        var price = new Money(request.Price, request.PriceCurrency);

        var item = new Item()
        {
            Name = request.Name,
            Price = price,
            Amount = request.Amount,
            Description = request.Description,
            Category = category
        };

        if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            item.Image = new Image { Url = request.ImageUrl };
        }

        context.Items.Add(item);
        await context.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
