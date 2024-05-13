using Application.Common.Interface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries;

public record GetItemQuery(int id) : IRequest<ItemDto>;

public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        Item? item = await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken: cancellationToken);

        return _mapper.Map<ItemDto>(item);
    }
}
