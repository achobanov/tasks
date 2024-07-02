using Challenge.Common.Filesystem;
using Challenge.Common.Injection;
using Challenge.Domain.Core;
using Challenge.Domain.Files.Abstractions;
using Microsoft.Extensions.Options;

namespace Challenge.Domain.Files;

public class FileStorage : IFileStorage
{
    private readonly string _path;

    public FileStorage(IOptions<StorageConfiguration> options)
    {
        _path = options.Value.Directory.ToRootPath();
    }

    public async Task CreateFile(IPlainFile file)
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        var path = Path.Combine(_path, file.Name);
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
