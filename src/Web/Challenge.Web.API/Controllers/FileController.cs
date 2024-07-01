using Challenge.Common;
using Challenge.Domain.Abstractions;
using Challenge.Web.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Challenge.Common.Constants;

namespace Challenge.Web.API.Controllers;

[ApiController]
public class FileController : ControllerBase
{
    private readonly INotifier _notifier;

    public FileController(INotifier notifier)
    {
        _notifier = notifier;
    }

    [HttpPost(Endpoints.FILE_UPLOAD)]
    public Task<IActionResult> Upload([FromBody] FileUploadContract request)
    {
        foreach (var file in request.Files)
        {
            var bytes = Convert.FromBase64String(file.Payload);
            var encoding = Encoding.GetEncoding(file.EncodingName);
            var contents = encoding.GetString(bytes);
            var plainFile = file.Decode(_notifier);
            var document = XmlProcessor.Parse(plainFile);
        }
        return Task.FromResult(Ok() as IActionResult);
    }
}
