using FluentValidation;
namespace Application.Items.Commands;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(v => v.CategoryId)
            .NotEmpty();

        RuleFor(v => v.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(v => v.PriceCurrency)
            .NotEmpty();

        RuleFor(v => v.Amount)
            .GreaterThan(0);
    }
}
