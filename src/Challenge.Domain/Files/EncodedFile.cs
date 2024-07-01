using Challenge.Common;

namespace Challenge.Domain.Files;

public record struct EncodedFile(string Name, string Payload, string EncodingName)
{
    public PlainFile Decode(INotifier notifier)
    {
        var bytes = Convert.FromBase64String(Payload);
        FilesizeValidator.Validate(bytes, Name);

        var encoding = EncodingProvider.GetEncodingOrUtf8(notifier, EncodingName);
        var contents = encoding.GetString(bytes);
        return new PlainFile(Name, contents);
    }
}
