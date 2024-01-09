using Microsoft.Extensions.Configuration;

namespace VL.Challenge.Storage;

internal class StorageConfiguration
{
    private const string STORAGE_SECTION = "Storage";
    private const string ENV_SQL_HOST = "SQL_HOST";

    public StorageConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSection(STORAGE_SECTION)
            ?? throw new Exception($"Cannot configure storage. Section '{STORAGE_SECTION}' is required missing");
        var sqlHost = Environment.GetEnvironmentVariable(ENV_SQL_HOST)
            ?? throw new Exception($"Cannot configure storage. Enrionment variable '{ENV_SQL_HOST}' is missing");

        var sectionModel = section.Get<Section>()
            ?? throw new Exception($"Cannot configure storage. Section '{STORAGE_SECTION}' is invalid");

        ConnectionString = string.Format(sectionModel.ConnectionStringTemplate, sqlHost);
    }

    public string ConnectionString { get; }

    private class Section
    {
        public string ConnectionStringTemplate { get; init; } = default!;
    }
}
