using StackExchange.Redis;

namespace VL.Challenge.API.Redis;

public class RedisContext
{
    public RedisContext(RedisConfiguration configuration)
    {
        var options = ConfigurationOptions.Parse(configuration.ConnectionString);
        Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options)).Value;
    }

    public ConnectionMultiplexer Connection { get; }
}