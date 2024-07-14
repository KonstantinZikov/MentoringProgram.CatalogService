using Infrastructure.Data;
using DAL.Common.Interface;
using DAL.Data.Configurations;
using DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDalServices(this IServiceCollection services)
    {
        string connectionString = $"default.db";

        services.AddSingleton<ILiteDatabaseConfiguration>(new LiteDatabaseConfiguration(connectionString));
        services.AddScoped<IRepository<Cart,string>,Repository<Cart, string>>();

        return services;
    }
}
