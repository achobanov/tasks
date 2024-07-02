namespace Challenge.Common.Filesystem;

public static class PathExtensions
{
    public static string ToRootPath(this string path)
    {
        if (Path.IsPathRooted(path))
        {
            return path;
        }
        return Path.Combine(Directory.GetCurrentDirectory(), path); 
    }
}
