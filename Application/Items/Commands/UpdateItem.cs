using Application.Common.Interface;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Events;
using Domain.ValueObjects;
using MediatR;

namespace Application.Items.Commands;

public record UpdateItemCommand : IRequest
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public string? ImageUrl { get; init; }

    public required int CategoryId { get; init; }

    public required int Price { get; init; }

    public required string PriceCurrency { get; init; }

    public required int Amount { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        Item? item = await _context.Items
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, item);

        if (item != null)
        {
            item.Name = request.Name;
            item.Amount = request.Amount;
            item.Description = request.Description;

            Category? category = _context.Categories.FindAsync([request.CategoryId], cancellationToken).Result;

            Guard.Against.NotFound(request.CategoryId, category);

            item.Category = category;

            item.Price = new Money(request.Price, request.PriceCurrency);
            item.Image = string.IsNullOrEmpty(request.ImageUrl) ? null : new Image { Url = request.ImageUrl };

            item.AddDomainEvent(new ItemUpdatedEvent(item));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
