using Challenge.Common;
using Challenge.Common.Injection;
using Challenge.Domain.Files;
using Challenge.Web.Client.Toasts;
using Microsoft.AspNetCore.Components.Forms;
using System.Text;

namespace Challenge.Web.Client.Files;

public partial class FileReader : IFileReader
{
    private readonly IToaster _toaster;
    private static readonly int STREAM_SIZE_LIMIT = 2 * Constants.MegaByte;

    public FileReader(IToaster toaster)
    {
        _toaster = toaster;
    }

    public async Task<EncodedFile> Read(IBrowserFile browserFile)
    {
        using var stream = browserFile.OpenReadStream(maxAllowedSize: STREAM_SIZE_LIMIT);
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();
        var plainFile = new PlainFile(browserFile.Name, content);
        var encodedFile = plainFile.Encode(_toaster);
        return encodedFile;
    }
}

public interface IFileReader : ITransient
{
    Task<EncodedFile> Read(IBrowserFile browserFile);
}
