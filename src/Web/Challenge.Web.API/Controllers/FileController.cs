using Challenge.Common.Contravts;
using Challenge.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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
            var bytes = Convert.FromBase64String(file.Content);
            var encoding = Encoding.GetEncoding(file.EncodingName);
            var contents = encoding.GetString(bytes);
            ;
            //XmlValidator.Validate(contents);
        }
        return Task.FromResult(Ok() as IActionResult);
    }
}
