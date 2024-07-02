using Challenge.Common;
using Challenge.Common.Injection;
using Challenge.Domain.Core;
using Challenge.Domain.Files;
using Challenge.Domain.Files.Objects;
using Challenge.Web.Client.Toasts;
using Microsoft.AspNetCore.Components.Forms;

namespace Challenge.Web.Client.Files;

public partial class FileReader : IFileReader
{
    private readonly IToaster _toaster;
    private static readonly int STREAM_SIZE_LIMIT = 2 * Constants.MegaByte;

    public FileReader(IToaster toaster)
    {
        _toaster = toaster;
    }

    public async Task<XmlEncodedFile?> ReadXml(IBrowserFile browserFile)
    {
        using var stream = browserFile.OpenReadStream(maxAllowedSize: STREAM_SIZE_LIMIT);
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();
        try
        {
            var plainFile = new XmlPlainFile(browserFile.Name, content);
            return (XmlEncodedFile)plainFile.Encode(_toaster);
        }
        catch (DomainException validation)
        {
            await _toaster.Validation(validation.Message, validation.StackTrace);
            return null;
        }
    }
}

public interface IFileReader : ITransient
{
    Task<XmlEncodedFile?> ReadXml(IBrowserFile browserFile);
}
