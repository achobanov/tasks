using System.Xml;

namespace Challenge.Domain.Abstractions;

public static class XmlValidator
{
    public static bool Validate(FileModel file)
    {
        var document = new XmlDocument();
        try
        {
            document.LoadXml(file.Content);
            return true;
        }
        catch (XmlException)
        {
            throw new DomainException($"File '{file.Filename}' is not a valid XML file");
        }
    }
}
