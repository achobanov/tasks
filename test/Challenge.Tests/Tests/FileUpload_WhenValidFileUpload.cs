using Challenge.Tests.Helpers;
using Challenge.Web.API.Controllers;
using MyTested.AspNetCore.Mvc;

namespace Challenge.Tests.Tests;

public class FileUpload_WhenValidFileUpload
{
    public FileUpload_WhenValidFileUpload()
    {
        FilesystemHelper.DeleteFiles(SettingsHelper.GetStoragePath());
    }

    [Fact]
    public void ShouldReturnResponse()
    {
        var request = FileUploadHelper.BuildXmlFileUploadRequest("valid.xml");
        var response = FileUploadHelper.BuildExpectedResponse(request);

        MyMvc
            .Controller<FileController>()
            .Calling(x => x.Upload(request))
            .ShouldReturn()
            .Ok(response);
    }

    [Fact]
    public void ShouldNotPersistFile()
    {
        lock (Locker.Lock)
        {
            var filename = "valid";
            var request = FileUploadHelper.BuildXmlFileUploadRequest($"{filename}.xml");
            var response = FileUploadHelper.BuildExpectedResponse(request);

            MyMvc
                .Controller<FileController>()
                .Calling(x => x.Upload(request))
                .ShouldReturn()
                .Ok(response);

            Assert.True(FilesystemHelper.StorageFileExists($"{filename}.json"));
        }
    }
}
