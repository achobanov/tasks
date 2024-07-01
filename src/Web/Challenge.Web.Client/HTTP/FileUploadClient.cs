using Challenge.Common.HTTP;
using Challenge.Common.JSON;
using Challenge.Web.Client.Files;
using Challenge.Web.Client.Toasts;
using Challenge.Web.Common.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using static Challenge.Common.Constants;

namespace Challenge.Web.Client.HTTP;

public class FileUploadClient : HttpClientBase
{
    private readonly IFileReader _fileReader;

    public FileUploadClient(HttpClient client, IFileReader fileReader, IToaster toaster) : base(client, toaster)
    {
        _fileReader = fileReader;
    }

    public async Task<IEnumerable<FileModel>> Upload(IEnumerable<IBrowserFile> borwserFiles)
    {
        var fileTasks = borwserFiles.Select(_fileReader.ReadXml);
        var files = (await Task.WhenAll(fileTasks))
            .Where(x => x.HasValue)
            .Select(x => x!.Value);

        var contract = new FileUploadContract(files);
        return await Post(Endpoints.FILE_UPLOAD, contract)
            .FromJson<IEnumerable<FileModel>>();
    }
}
