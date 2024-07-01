using Newtonsoft.Json;
using System.Xml;

namespace Challenge.Domain.Converters;

public class XmlToJsonConverter
{
    public static string Convert(XmlDocument xmlDocument)
    {
        return JsonConvert.SerializeXmlNode(xmlDocument);
    }
}
