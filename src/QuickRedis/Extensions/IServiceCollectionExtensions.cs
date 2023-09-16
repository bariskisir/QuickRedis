using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using QuickRedis.Services.Definitions;
using QuickRedis.Services.Implementations;
namespace QuickRedis.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddQuickRedis(this IServiceCollection services, Action<RedisCacheOptions> settings)
    {
        services.AddStackExchangeRedisCache(settings);
        services.AddSingleton<IQuickRedisService, QuickRedisService>();
        return services;
    }
}
