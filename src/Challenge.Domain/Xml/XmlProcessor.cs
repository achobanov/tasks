using Challenge.Domain.Core;
using Challenge.Domain.Files;
using System.Xml;

namespace Challenge.Domain.Xml;

public static class XmlProcessor
{
    public static XmlDocument Parse(PlainFile file)
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
