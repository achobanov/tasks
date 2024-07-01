using Challenge.Common.Injection;
using Challenge.Domain.Core;
using Challenge.Domain.Files;
using Challenge.Domain.Processors;
using System.Text;

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

    public async Task Dispatch(IEnumerable<EncodedFile> files)
    {
        var tasks = new List<Task>();
        foreach (var file in files)
        {
            var task = Task.Run(() => InterceptValidations(() => _xmlUpload.Process(file)));
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);
        if (_validations.Any())
        {
            AggregateValidations(files.Count());
        }
    }

    public async Task InterceptValidations(Func<Task> func)
    {
        try
        {
            await func();
        }
        catch (DomainException ex)
        {
            lock (_lock)
            {
                _validations.Add(ex);
            }
        }
    }

    private void AggregateValidations(int filesCount)
    {
        var successfulFileCount = filesCount - _validations.Count;
        throw new DomainAggregateException(successfulFileCount, _validations);
    }
}

public interface IUploadDispatcher : ITransient
{
    Task Dispatch(IEnumerable<EncodedFile> files);
}
