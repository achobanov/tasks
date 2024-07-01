using Challenge.Domain.Core;
using Challenge.Domain.Files;
using System.Xml;

namespace Challenge.Domain.Xml;

public static class XmlParser
{
    public static XmlDocument Parse(IPlainFile file)
    {
        var document = new XmlDocument();
        try
        {
            document.LoadXml(file.Content);
            return document;
        }
        catch (XmlException)
        {
            throw new DomainException($"File '{file.Name}' is not a valid XML file");
        }
    }
}
