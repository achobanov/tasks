using Challenge.Common.Injection;
using Challenge.Domain.Files;
using Challenge.Domain.Processors;

namespace Challenge.Domain.Dispatchers;

public class UploadDispatcher : IUploadDispatcher
{
    private readonly IXmlUploadProcessor _xmlUpload;

    public UploadDispatcher(IXmlUploadProcessor xmlUpload)
    {
        _xmlUpload = xmlUpload;
    }

    public async Task Dispatch(IEnumerable<EncodedFile> files)
    {
        var tasks = new List<Task>();
        foreach (var file in files)
        {
            var task = Task.Run(() => _xmlUpload.Process(file));
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);
    }
}

public interface IUploadDispatcher : ITransient
{
    Task Dispatch(IEnumerable<EncodedFile> files);
}
