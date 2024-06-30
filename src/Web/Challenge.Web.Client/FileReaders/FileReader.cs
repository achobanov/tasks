using Challenge.Common.Injection;
using Challenge.Domain;
using Challenge.Web.Client.FileReaders;
using Challenge.Web.Client.Toasts;
using Microsoft.AspNetCore.Components.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace Challenge.Web.Client.Services;

public partial class FileReader : IFileReader
{
    private readonly Regex encodingMatcher = Patterns.XmlEncoding();
    private readonly IToaster _toaster;

    public FileReader(IToaster toaster)
    {
        _toaster = toaster;
    }

    public async Task<FileModel> Read(IBrowserFile browserFile)
    {
        using var stream = browserFile.OpenReadStream();
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();
        var (base64Content, encoding) = Base64Encode(content);
        return new FileModel(browserFile.Name, base64Content, encoding.WebName);
    }

    private (string base64Content, Encoding encoding) Base64Encode(string input)
    {
        var encoding = GetEncoding(input);
        var bytes = encoding.GetBytes(input);
        return (Convert.ToBase64String(bytes), encoding);
    }

    private Encoding GetEncoding(string input)
    {
        var match = encodingMatcher.Match(input);
        if (!match.Success)
        {
            _toaster.Add("Missing encoding", "Defaulting to UTF8", UiColor.Warning);
            return Encoding.UTF8;
        }
        var encodingName = match.Groups[1].Value;
        try
        {
            return Encoding.GetEncoding(encodingName);
        }
        catch (ArgumentException)
        {
            _toaster.Add("Unsupported encoding", $"Encoding '{encodingName}' is not supported. Defaulting to UTF8", UiColor.Warning, 15);
            return Encoding.UTF8;
        }
    }
}

public interface IFileReader : ITransient
{
    Task<FileModel> Read(IBrowserFile browserFile);
}
