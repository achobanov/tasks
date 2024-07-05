using Challenge.Common.HTTP;
using Challenge.Common.JSON;
using Challenge.Domain.Files.Objects;
using Challenge.Web.Client.Files;
using Challenge.Web.Client.Toasts;
using Challenge.Web.Common.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using static Challenge.Common.Constants;

namespace Challenge.Web.Client.HTTP;

public class FileUploadClient : HttpClientBase
{
    private readonly IFileReader _fileReader;
    private readonly IToaster _toaster;

    public FileUploadClient(HttpClient client, IFileReader fileReader, IToaster toaster) : base(client, toaster)
    {
        _fileReader = fileReader;
        _toaster = toaster;
    }

    public async Task<IEnumerable<FileModel>> Upload(IEnumerable<IBrowserFile> borwserFiles)
    {
        var fileTasks = borwserFiles.Select(_fileReader.ReadXml);
        var files = (await Task.WhenAll(fileTasks))
            .Where(x => x.HasValue)
            .Select(x => x!.Value);

        var contract = new FileUploadContract(files);
        var result = await Post(Endpoints.FILE_UPLOAD, contract).FromJson<IEnumerable<JsonEncodedFile>>();

        return result
            .Select(x => x.Decode(_toaster))
            .Select(x => new FileModel(x.Name, x.Content));
    }

    public async Task<IEnumerable<FileModel>> GetFiles()
    {
        var encodedFiles = await Get<IEnumerable<JsonEncodedFile>>(Endpoints.FILE_UPLOAD);
        var decodedFiles = encodedFiles
            .Select(x => x.Decode(_toaster))
            .Select(x => new FileModel(x.Name, x.Content));
        return decodedFiles;
    }

    public async Task Delete(string name)
    {
        var query = new Dictionary<string, string> { { "filename", name } };
        await Delete(Endpoints.FILE_UPLOAD, query);
    }
}
