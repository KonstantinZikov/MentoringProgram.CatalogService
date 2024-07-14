using FluentValidation;
namespace BLL.Carts.Commands;

public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator()
    {
        RuleFor(v => v.ProductId)
            .NotEmpty();

        RuleFor(v => v.Name)
            .NotEmpty();

        RuleFor(v => v.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(v => v.PriceCurrency)
            .NotEmpty();

        RuleFor(v => v.Quantity)
            .GreaterThanOrEqualTo(0);
    }
}
