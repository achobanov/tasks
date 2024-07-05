using Challenge.Common.Filesystem;
using Challenge.Common.Injection;
using Challenge.Domain.Core;
using Challenge.Domain.Files.Abstractions;
using Challenge.Domain.Files.Objects;
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
        CreateDirectoryIfNotExists();

        var path = Path.Combine(_path, file.Name);
        if (File.Exists(path))
        {
            throw new DomainException($"File '{file.Name}' already exists");
        }
        await File.WriteAllTextAsync(path, file.Content);
    }

    public async Task<IEnumerable<IPlainFile>> GetFiles()
    {
        CreateDirectoryIfNotExists();

        var filePaths = Directory.GetFiles(_path);
        var tasks = new List<(string, Task<string>)>();
        foreach (var path in filePaths)
        {
            var name = Path.GetFileName(path);
            var task = File.ReadAllTextAsync(path);
            tasks.Add((name, task));
        }
        await Task.WhenAll(tasks.Select(x => x.Item2));

        var files = tasks.Select(x => (IPlainFile)new JsonFromXmlPlainFile(x.Item1, x.Item2.Result));
        return files;
    }

    public void CreateDirectoryIfNotExists()
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }
}

public interface IFileStorage : ITransient
{
    Task CreateFile(IPlainFile file);
    Task<IEnumerable<IPlainFile>> GetFiles();
}
