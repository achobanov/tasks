using Challenge.Common;
using Challenge.Domain.Files.Abstractions;
using System.Text.Json.Serialization;

namespace Challenge.Domain.Files.Objects;

public record struct JsonEncodedFile : IEncodedFile
{
    [JsonConstructor]
    public JsonEncodedFile(string name, string content, string encodingName)
    {
        Name = new JsonFilename(name);
        Content = content;
        EncodingName = encodingName;
    }

    public string Name { get; }
    public string Content { get; }
    public string EncodingName { get; }

    public IPlainFile Decode(INotifier notifier, bool validate = true)
    {
        var decoted = this.Base64Decode(notifier, validate);
        return new JsonFromXmlPlainFile(Name, decoted);
    }
}