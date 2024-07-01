using Challenge.Common;
using Challenge.Domain.Converters;
using Challenge.Domain.Files;
using Challenge.Domain.Xml;
using Challenge.Web.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Challenge.Common.Constants;

namespace Challenge.Web.API.Controllers;

[ApiController]
public class FileController : ControllerBase
{
    private readonly INotifier _notifier;

    private List<string> _testFilenames = new List<string>
    {
        @"../../somefile.txt",
        @"../../otherfile.exe",
        @"..\..\thirdfile.json",
        @"../..\fourthfile.test",
        @"../../../fifth",
        @"..\..\..\sixth.exe",
        @"%CommonProgramFiles%\seventh",
        @"%PATH%/eight.bat",
        @"%PATH%/dir/eight.bat",
        @"~/../../nineth.sh",
        @"~/tenth.sh",
        @"/etc/something",
        @"/etc/bin/something.sh",
        @"/d/one/two.exe",
        @"/c/one/three.exe",
        @"\d\one\four.exe",
        @"%cd%/file.one"
    };

    public FileController(INotifier notifier)
    {
        _notifier = notifier;
    }

    [HttpPost(Endpoints.FILE_UPLOAD)]
    public Task<IActionResult> Upload([FromBody] FileUploadContract request)
    {
        foreach (var test in _testFilenames)
        {
            var filename = new JsonFilename(test);
            ;
        }

        foreach (var file in request.Files)
        {
            var bytes = Convert.FromBase64String(file.Payload);
            var encoding = Encoding.GetEncoding(file.EncodingName);
            var contents = encoding.GetString(bytes);
            var plainFile = file.Decode(_notifier);
            var document = XmlProcessor.Parse(plainFile);
            var json = XmlToJsonConverter.Convert(document);
            var filename = new JsonFilename(file.Name);
            ;
        }
        return Task.FromResult(Ok() as IActionResult);
    }
}
