using Challenge.Common;
using Challenge.Domain.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace Challenge.Domain.Files;

public record struct PlainFile(string Name, string Content)
{
    private readonly Regex _encodingMatcher = Patterns.XmlEncoding();

    public EncodedFile Encode(INotifier notifier)
    {
        var encoding = MatchEncoding(notifier);
        var bytes = encoding.GetBytes(Content);

        var base64Encoded = Convert.ToBase64String(bytes);
        return new EncodedFile(Name, base64Encoded, encoding.WebName);
    }

    private Encoding MatchEncoding(INotifier notifier)
    {
        var match = _encodingMatcher.Match(Content);
        if (!match.Success)
        {
            notifier.Validation("Missing encoding", "Defaulting to UTF8");
            return Encoding.UTF8;
        }
        var encodingName = match.Groups[1].Value;
        return EncodingProvider.GetEncodingOrUtf8(notifier, encodingName);
    }
}