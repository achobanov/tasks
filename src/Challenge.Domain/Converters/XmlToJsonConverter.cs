using Challenge.Common.JSON;
using Challenge.Domain.Files.Abstractions;
using Challenge.Domain.Xml;

namespace Challenge.Domain.Converters;

public class XmlToJsonConverter
{
    public static string Convert(IPlainFile file)
    {
        var xmlDocument = XmlParser.Parse(file);
        return xmlDocument.ToJson();
    }
}
