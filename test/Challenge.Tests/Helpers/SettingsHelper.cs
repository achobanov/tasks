using Challenge.Common.Filesystem;
using Challenge.Common.JSON;

namespace Challenge.Tests.Helpers;

internal static class SettingsHelper
{
    public static string GetStoragePath()
    {
        var settingsContent = File.ReadAllText("testsettings.json".ToRootPath());
        var settings = settingsContent.FromJson<TestSettings>();
        return settings.StorageConfiguration.Directory;
    }
}
