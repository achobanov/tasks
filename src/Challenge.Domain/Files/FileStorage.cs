using Challenge.Common.Injection;
using Challenge.Domain.Core;

namespace Challenge.Domain.Files;

public class FileStorage : IFileStorage
{
    private const string STORE_ROOT = "/c/tmp/challenge-ds";

    public async Task CreateFile(Filename filename, string content)
    {
        if (!Directory.Exists(STORE_ROOT))
        {
            Directory.CreateDirectory(STORE_ROOT);
        }
        var path = Path.Combine(STORE_ROOT, filename.ToString());
        if (File.Exists(path))
        {
            throw new DomainException($"File '{filename}' already exists");
        }
        await File.WriteAllTextAsync(path, content);
    }
}

public interface IFileStorage : ITransient
{
    Task CreateFile(Filename path, string content);
}
