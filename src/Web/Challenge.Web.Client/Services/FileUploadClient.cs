using Challenge.Common.Contravts;
using Challenge.Common.HTTP;
using Microsoft.AspNetCore.Components.Forms;
using static Challenge.Common.Constants;

namespace Challenge.Web.Client.Services;

public class FileUploadClient : HttpClientBase
{
    private readonly IFileReader _fileReader;

    public FileUploadClient(HttpClient client, IFileReader fileReader) : base(client)
    {
        _fileReader = fileReader;
    }

    public async Task Upload(IEnumerable<IBrowserFile> files)
    {
        var fileTasks = files.Select(_fileReader.Read);
        var filesModels = await Task.WhenAll(fileTasks);

        var contract = new FileUploadContract(filesModels);
        await Post(Endpoints.FILE_UPLOAD, contract);
    }
}
