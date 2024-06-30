using System.Xml;

namespace Challenge.Domain.Abstractions;

public static class XmlValidator
{
    public static void Validate(FileModel file)
    {
        var document = new XmlDocument();
        try
        {
            document.LoadXml(file.Content);
        }
        catch (XmlException)
        {
            throw new DomainException($"File '{file.Filename}' is not a valid XML file");
        }
    }
}
