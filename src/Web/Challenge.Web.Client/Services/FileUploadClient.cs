using Challenge.Common.Contravts;
using Challenge.Common.HTTP;
using Microsoft.AspNetCore.Components.Forms;
using static Challenge.Common.Constants;

namespace Challenge.Web.Client.Services;

public class FileUploadClient : HttpClientBase
{
    public FileUploadClient(HttpClient client) : base(client)
    {
    }

    public async Task Upload(IBrowserFile file)
    {
        using var stream = file.OpenReadStream();
        using var reader  = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        var contract = new FileUploadContract(file.Name, content);
        await Post(Endpoints.FILE_UPLOAD, contract);
    }
}
