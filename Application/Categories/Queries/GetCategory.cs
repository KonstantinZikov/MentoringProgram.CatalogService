using Application.Common.Identity;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record GetCategoryQuery(int id) : IRequest<CategoryDto>;

public class GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetCategoryQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        Category? category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken: cancellationToken);
        return mapper.Map<CategoryDto>(category);
    }
}
