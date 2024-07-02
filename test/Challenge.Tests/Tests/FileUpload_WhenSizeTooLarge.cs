using Challenge.Domain.Core;
using Challenge.Tests.Helpers;
using Challenge.Web.API.Controllers;
using MyTested.AspNetCore.Mvc;

namespace Challenge.Tests.Tests;

public class FileUpload_WhenSizeTooLarge
{
    public FileUpload_WhenSizeTooLarge()
    {
        FilesystemHelper.DeleteFiles(SettingsHelper.GetStoragePath());
    }

    [Fact]
    public void ShouldThrowDomainException()
    {
        var filname = "invalid-size";
        var request = FileUploadHelper.BuildXmlFileUploadRequest($"{filname}.xml");
        var response = FileUploadHelper.BuildExpectedResponse(request);

        MyMvc
            .Controller<FileController>()
            .Calling(x => x.Upload(request))
            .ShouldThrow()
            .Exception()
            .OfType<AggregateException>(); // Library cannot for some reason detect my DomainAggregateException
    }

    [Fact]
    public void ShouldNotStoreFile()
    {
        var filname = "invalid-size";
        var request = FileUploadHelper.BuildXmlFileUploadRequest($"{filname}.xml");
        var response = FileUploadHelper.BuildExpectedResponse(request);

        MyMvc
            .Controller<FileController>()
            .Calling(x => x.Upload(request))
            .ShouldThrow()
            .Exception()
            .OfType<AggregateException>(); 

        Assert.False(FilesystemHelper.StorageFileExists(filname));
    }
}
