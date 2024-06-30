using Challenge.Common.Contravts;
using Challenge.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using static Challenge.Common.Constants;

namespace Challenge.Web.API.Controllers;

[ApiController]
public class FileController : ControllerBase
{
    [HttpPost(Endpoints.FILE_UPLOAD)]
    public Task<IActionResult> Upload([FromBody] FileUploadContract request)
    {
        foreach (var file in request.Files)
        {
            XmlValidator.Validate(file);
        }
        return Task.FromResult(Ok() as IActionResult);
    }
}
