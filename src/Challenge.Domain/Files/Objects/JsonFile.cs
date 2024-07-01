using Challenge.Common;
using Challenge.Common.JSON;
using Newtonsoft.Json;

namespace Challenge.Domain.Files.Objects;

public record struct JsonEncodedFile(string Name, string Content, string EncodingName) : IEncodedFile
{
    public IPlainFile Decode(INotifier notifier)
    {
        var decoted = this.Base64Decode(notifier);
        return new JsonFromXmlPlainFile(Name, decoted);
    }
}

public record struct JsonFromXmlPlainFile(string Name, string Content) : IPlainFile
{
    public IEncodedFile Encode(INotifier notifier)
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
