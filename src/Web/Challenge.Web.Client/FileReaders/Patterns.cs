using System.Text.RegularExpressions;

namespace Challenge.Web.Client.FileReaders;

public static partial class Patterns
{
    private const string ENCODING_PATTERN = @"<\?xml\s+version=""[0-9]{1}.[0-9]{1}""\s+encoding=""(.*)""\s*\?>";
    [GeneratedRegex(ENCODING_PATTERN)]
    public static partial Regex XmlEncoding();
}
