using Challenge.Common;
using Challenge.Common.JSON;
using Newtonsoft.Json;

namespace Challenge.Domain.Files.Objects;

public record struct JsonEncodedFile : IEncodedFile
{
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

public record struct JsonFromXmlPlainFile : IPlainFile
{
    public JsonFromXmlPlainFile(string name, string content)
    {
        Name = new JsonFilename(name);
        Content = content;
    }

    public string Name { get; }
    public string Content { get; }

    public IEncodedFile Encode(INotifier notifier, bool validate = false)
    {
        var encodingName = Content.FromJson<JsonMetaModel>().XmlRoot.Encoding;
        var encoding = EncodingProvider.GetEncodingOrUtf8(notifier, encodingName);
        var encoded = this.Base64Encode(encoding);
        return new JsonEncodedFile(Name, encoded, encodingName);
    }
}

public record struct JsonMetaModel
{
    [JsonProperty("?xml")]
    public XmlRootModel XmlRoot { get; set; }
}

public record struct XmlRootModel
{
    [JsonProperty("@version")]
    public string Version { get; set; }
    [JsonProperty("@encoding")]
    public string Encoding { get; set; }
}
