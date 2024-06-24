using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }

    DbSet<Item> Items { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
