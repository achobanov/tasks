using Challenge.Tests.Helpers;
using Challenge.Web.API.Controllers;
using MyTested.AspNetCore.Mvc;

namespace Challenge.Tests.Tests;

public class FileUpload_Success
{
    public FileUpload_Success()
    {
        SettingsHelper.ClearStoredFiles();
    }

    [Fact]
    public void Test()
    {
        var request = FileUploadHelper.BuildXmlFileUploadRequest("valid.xml");
        var response = FileUploadHelper.BuildExpectedResponse(request);

        MyMvc
            .Controller<FileController>()
            .Calling(x => x.Upload(request))
            .ShouldReturn()
            .Ok(response);
    }
}
