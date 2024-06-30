using Challenge.Common.Contravts;
using Microsoft.AspNetCore.Mvc;
using static Challenge.Common.Constants;

namespace Challenge.Web.API.Controllers;

[ApiController]
public class FileController : ControllerBase
{
    [HttpPost(Endpoints.FILE_UPLOAD)]
    public Task<IActionResult> Upload([FromBody] FileUploadContract request)
    {
        return Task.FromResult(Ok() as IActionResult);
    }
}
