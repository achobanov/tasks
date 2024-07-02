using Challenge.Common.Filesystem;

namespace Challenge.Tests.Helpers;

internal static class FilesystemHelper
{
    public static string ReadTestInputFile(string filename)
    {
        return File.ReadAllText($"files/{filename}".ToRootPath());
    }
}
