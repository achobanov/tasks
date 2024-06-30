using Challenge.Common.Contravts;
using Challenge.Common.Injection;
using Microsoft.AspNetCore.Components.Forms;

namespace Challenge.Web.Client.Services;

public class FileReader : IFileReader
{
    public async Task<FileModel> Read(IBrowserFile browserFile)
    {
        using var stream = browserFile.OpenReadStream();
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();
        return new FileModel(browserFile.Name, content);
    }
}

public interface IFileReader : ITransient
{
    Task<FileModel> Read(IBrowserFile browserFile);
}
