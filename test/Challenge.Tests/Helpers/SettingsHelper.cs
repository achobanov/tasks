using Challenge.Common.Filesystem;
using Challenge.Common.JSON;

namespace Challenge.Tests.Helpers;

internal static class SettingsHelper
{
    public static void ClearStoredFiles()
    {
        var settingsContent = File.ReadAllText("testsettings.json".ToRootPath());
        var settings = settingsContent.FromJson<TestSettings>();
        if (!Directory.Exists(settings.StorageConfiguration.Directory))
        {
            return;
        }
        var directoryInfo = new DirectoryInfo(settings.StorageConfiguration.Directory);
        foreach (var file in directoryInfo.EnumerateFiles())
        {
            file.Delete();
        }
    }
}
