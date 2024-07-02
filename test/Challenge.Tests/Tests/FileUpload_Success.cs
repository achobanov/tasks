using Challenge.Common.Filesystem;
using Challenge.Common.JSON;
using Challenge.Tests.Helpers;
using Challenge.Web.API.Controllers;
using MyTested.AspNetCore.Mvc;

namespace Challenge.Tests.Tests;

public class FileUpload_Success
{
    public FileUpload_Success()
    {
        var settingsContent = File.ReadAllText("testsettings.json".ToRootPath());
        var settings = settingsContent.FromJson<TestSettings>();
        var directoryInfo = new DirectoryInfo(settings.StorageConfiguration.Directory);
        foreach (var file in directoryInfo.EnumerateFiles())
        {
            file.Delete();
        }
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
