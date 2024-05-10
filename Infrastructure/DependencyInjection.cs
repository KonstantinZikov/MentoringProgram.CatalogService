using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Common.Interface;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        string connectionString = $"Data Source=catalogService.db";

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlite(connectionString, b => b.MigrationsAssembly(nameof(Infrastructure)));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
