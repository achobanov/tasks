using Challenge.Common.Injection;
using Challenge.Domain.Core;
using Challenge.Domain.Files.Abstractions;
using Challenge.Domain.Files.Objects;
using Challenge.Domain.Processors;

namespace Challenge.Domain.Dispatchers;

public class UploadDispatcher : IUploadDispatcher
{
    private readonly IXmlUploadProcessor _xmlUpload;
    private List<DomainException> _validations = new();
    private object _lock = new();

    public UploadDispatcher(IXmlUploadProcessor xmlUpload)
    {
        _xmlUpload = xmlUpload;
    }

    public async Task<IEnumerable<IPlainFile>> Dispatch(IEnumerable<XmlEncodedFile> files)
    {
        var tasks = new List<Task<IPlainFile?>>();
        foreach (var file in files)
        {
            var task = Task.Run(() => InterceptValidations(() => _xmlUpload.Process(file))); // TODO remove Task.Run
            tasks.Add(task);
        }
        var resultFiles = (await Task.WhenAll(tasks))
            .Where(x => x != null)
            .Select(x => x!)
            .ToList();

        if (_validations.Any())
        {
            AggregateValidations(resultFiles.Count);
        }

        return resultFiles;
    }

    public async Task<IPlainFile?> InterceptValidations(Func<Task<IPlainFile>> func)
    {
        try
        {
            return await func();
        }
        catch (DomainException ex)
        {
            lock (_lock)
            {
                _validations.Add(ex);
            }
            return null;
        }
    }

    private void AggregateValidations(int filesCount)
    {
        var successfulFileCount = filesCount - _validations.Count;
        var message = $"Uploaded '{successfulFileCount}' files successfully and raised '{_validations.Count()}' validation errors";
        throw new DomainAggregateException(message, _validations);
    }
}

public interface IUploadDispatcher : ITransient
{
    Task<IEnumerable<IPlainFile>> Dispatch(IEnumerable<XmlEncodedFile> files);
}
