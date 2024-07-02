using Challenge.Common.Filesystem;

namespace Challenge.Tests.Helpers;

internal static class FilesystemHelper
{

    public static string ReadTestInputFile(string filename)
    {
        return File.ReadAllText($"files/{filename}".ToRootPath());
    }

    public static void DeleteFiles(string path)
    {
        lock (Locker.Lock)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            var directoryInfo = new DirectoryInfo(path);
            foreach (var file in directoryInfo.EnumerateFiles())
            {
                file.Delete();
            }
        }
    }

    public static bool StorageFileExists(string path)
    {
        return File.Exists(Path.Combine(SettingsHelper.GetStoragePath(), path));
    }
}
