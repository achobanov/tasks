using Challenge.Common;
using Challenge.Common.Injection;
using Challenge.Domain.Converters;
using Challenge.Domain.Files;
using Challenge.Domain.Files.Objects;

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

    public async Task<IPlainFile> Process(XmlEncodedFile file)
    {
        var plainFile = file.Decode(_notifier);
        var json = XmlToJsonConverter.Convert(plainFile);
        var filename = new JsonFilename(plainFile.Name);
        var jsonFile = new JsonFromXmlPlainFile(filename, json);
        await _fileStorage.CreateFile(jsonFile);

        return jsonFile;
    }
}

public interface IXmlUploadProcessor : ITransient
{
    Task<IPlainFile> Process(XmlEncodedFile file);
}
