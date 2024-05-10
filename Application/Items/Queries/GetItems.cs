using Application.Common.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries;

public record GetItemsQuery : IRequest<IReadOnlyList<ItemDto>>;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IReadOnlyList<ItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
       => await _context.Items
                .AsNoTracking()
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.Id)
                .ToListAsync(cancellationToken);

}
