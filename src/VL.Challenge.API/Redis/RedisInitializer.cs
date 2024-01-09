using StackExchange.Redis;
using VL.Challenge.Common;

namespace VL.Challenge.API.Redis;

public static class RedisInitializer
{
    public static IServiceCollection AddVLCache(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new RedisConfiguration(configuration);
        var context = new RedisContext(config);
        services.AddSingleton(context);
        services.AddScoped(x => x.GetRequiredService<RedisContext>().Connection.GetDatabase());
        services.AddScoped(typeof(ICache<>), typeof(RedisService<>));

        return services;
    }
}
