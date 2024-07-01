using Challenge.Common.Injection;
using Challenge.Domain.Core;

namespace Challenge.Domain.Files;

public class FileStorage : IFileStorage
{
    private const string STORE_ROOT = "/c/tmp/challenge-ds";

    public async Task CreateFile(IPlainFile file)
    {
        if (!Directory.Exists(STORE_ROOT))
        {
            Directory.CreateDirectory(STORE_ROOT);
        }
        var path = Path.Combine(STORE_ROOT, file.Name);
        if (File.Exists(path))
        {
            throw new DomainException($"File '{file.Name}' already exists");
        }
        await File.WriteAllTextAsync(path, file.Content);
    }
}

public interface IFileStorage : ITransient
{
    Task CreateFile(IPlainFile file);
}
