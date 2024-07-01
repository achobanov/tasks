using Challenge.Common;
using Challenge.Common.Injection;
using Challenge.Domain.Converters;
using Challenge.Domain.Files;

namespace Challenge.Domain.Processors;

public class XmlUploadProcessor : IXmlUploadProcessor
{
    private readonly INotifier _notifier;
    private readonly IFileStorage _fileStorage;

    public XmlUploadProcessor(INotifier notifier, IFileStorage fileStorage)
    {
        _notifier = notifier;
        _fileStorage = fileStorage;
    }

    public async Task Process(EncodedFile file)
    {
        var plainFile = file.Decode(_notifier);
        var json = XmlToJsonConverter.Convert(plainFile);
        var filename = new JsonFilename(plainFile.Name);
        await _fileStorage.CreateFile(filename, json);
    }
}

public interface IXmlUploadProcessor : ITransient
{
    Task Process(EncodedFile file);
}
