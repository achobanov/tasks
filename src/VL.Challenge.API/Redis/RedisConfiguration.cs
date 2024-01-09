namespace VL.Challenge.API.Redis;

public class RedisConfiguration
{
    private const string REDIS_SECTION = "Redis";
    private const string ENV_REDIS_HOST = "REDIS_HOST";

    public RedisConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSection(REDIS_SECTION)
            ?? throw new Exception($"Cannot configure Redis. Section '{REDIS_SECTION}' is required missing");
        var sqlHost = Environment.GetEnvironmentVariable(ENV_REDIS_HOST)
            ?? throw new Exception($"Cannot configure Redis. Enrionment variable '{ENV_REDIS_HOST}' is missing");

        var sectionModel = section.Get<Section>()
            ?? throw new Exception($"Cannot configure Redis. Section '{REDIS_SECTION}' is invalid");

        ConnectionString = string.Format(sectionModel.ConnectionStringTemplate, sqlHost);
    }

    public string ConnectionString { get; }

    private class Section
    {
        public string ConnectionStringTemplate { get; set; } = default!;
    }
}
