using Application.Items.Queries;
using FluentValidation;
namespace Application.Items.Commands;

public class GetItemsQueryValidator : AbstractValidator<GetItemsQuery>
{
    public GetItemsQueryValidator()
    {
        RuleFor(v => v.page)
            .GreaterThan(0);
    }
}
