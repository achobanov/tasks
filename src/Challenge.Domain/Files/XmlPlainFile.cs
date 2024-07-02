using Challenge.Common;
using Challenge.Domain.Files.Abstractions;
using Challenge.Domain.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace Challenge.Domain.Files;

public record struct XmlPlainFile(string Name, string Content) : IPlainFile
{
    private readonly Regex _encodingMatcher = XmlPatterns.XmlEncoding();

    public IEncodedFile Encode(INotifier notifier)
    {
        var encoding = MatchEncoding(notifier);
        var bytes = encoding.GetBytes(Content);
        FilesizeValidator.Validate(bytes, Name);

        var base64Encoded = Convert.ToBase64String(bytes);
        return new XmlEncodedFile(Name, base64Encoded, encoding.WebName);
    }

    private Encoding MatchEncoding(INotifier notifier)
    {
        var match = _encodingMatcher.Match(Content);
        if (!match.Success)
        {
            notifier.Information("Missing encoding: defaulting to UTF8");
            return Encoding.UTF8;
        }
        var encodingName = match.Groups[1].Value;
        return EncodingProvider.GetEncodingOrUtf8(notifier, encodingName);
    }
}

public interface IPlainFile : IFile
{
    IEncodedFile Encode(INotifier notifier);
}