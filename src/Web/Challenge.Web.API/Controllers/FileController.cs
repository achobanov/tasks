using Challenge.Common;
using Challenge.Domain.Dispatchers;
using Challenge.Web.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using static Challenge.Common.Constants;

namespace Challenge.Web.API.Controllers;

[ApiController]
public class FileController : ControllerBase
{
    private readonly INotifier _notifier;
    private readonly IUploadDispatcher _uploadDispatcher;

    public FileController(INotifier notifier, IUploadDispatcher uploadDispatcher)
    {
        _notifier = notifier;
        _uploadDispatcher = uploadDispatcher;
    }

    [HttpPost(Endpoints.FILE_UPLOAD)]
    public async Task<IActionResult> Upload([FromBody] FileUploadContract request)
    {
        var files = await _uploadDispatcher.Dispatch(request.Files);
        var encodedFiles = files.Select(x => x.Encode(_notifier));
        return Ok(encodedFiles);
    }
}
