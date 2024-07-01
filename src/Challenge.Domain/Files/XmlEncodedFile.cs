using Challenge.Common;
using Challenge.Domain.Files.Abstractions;

namespace Challenge.Domain.Files;

public record struct XmlEncodedFile(string Name, string Content, string EncodingName) : IEncodedFile
{
    public IPlainFile Decode(INotifier notifier)
    {
        var bytes = Convert.FromBase64String(Content);
        FilesizeValidator.Validate(bytes, Name);

        var encoding = EncodingProvider.GetEncodingOrUtf8(notifier, EncodingName);
        var contents = encoding.GetString(bytes);
        return new XmlPlainFile(Name, contents);
    }
}

public interface IEncodedFile : IFile
{
    string EncodingName { get; }
    IPlainFile Decode(INotifier notifier);
}
