using Challenge.Common;
using Challenge.Domain.Files.Abstractions;

namespace Challenge.Domain.Files;

public record struct XmlEncodedFile(string Name, string Content, string EncodingName) : IEncodedFile
{
    public IPlainFile Decode(INotifier notifier, bool validate = true)
    {
        var encoding = EncodingProvider.GetEncodingOrUtf8(notifier, EncodingName);
        var contents = this.Base64Decode(encoding, validate);
        return new XmlPlainFile(Name, contents);
    }
}

public interface IEncodedFile : IFile
{
    string EncodingName { get; }
    IPlainFile Decode(INotifier notifier, bool validate = true);
}
