using Challenge.Domain.Files;
using Challenge.Domain.Xml;
using Newtonsoft.Json;

namespace Challenge.Domain.Converters;

public class XmlToJsonConverter
{
    public static string Convert(IPlainFile file)
    {
        var xmlDocument = XmlParser.Parse(file);
        return JsonConvert.SerializeXmlNode(xmlDocument, Formatting.Indented);
    }
}
