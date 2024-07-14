using Ardalis.GuardClauses;
using BLL.Common.Identity;
using DAL.Common.Interface;
using DAL.Entities;
using DAL.ValueObjects;
using MediatR;

namespace BLL.Carts.Commands;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record AddItemToCartCommand : IRequest<int>
{
    public required string CartId { get; init; }

    public required int ProductId { get; init; }

    public required string Name { get; init; }

    public string? ImageUrl { get; init; }

    public string? ImageAlt { get; init; }

    public decimal Price { get; set; }

    public required string PriceCurrency { get; set; }

    public required int Quantity { get; init; }
}

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, int>
{
    private readonly IRepository<Cart, string> _repository;

    public AddItemToCartCommandHandler(IRepository<Cart, string> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _repository.GetById(request.CartId);

        Guard.Against.NotFound(request.CartId, cart);

        Image? image = null;

        if (request.ImageUrl != null)
        {
            image = new Image { Url = request.ImageUrl, Alt = request.ImageAlt };
        }

        var lineItem = new LineItem
        {
            Id = cart.LinesIdCounter++,
            ProductId = request.ProductId,
            Name = request.Name,
            Quantity = request.Quantity,
            Image = image,
            Price = new Money(request.Price, request.PriceCurrency)
        };

        cart.Items.Add(lineItem);

        await _repository.Update(cart);

        return lineItem.Id;
    }
}
