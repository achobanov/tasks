using Challenge.Tests.Helpers;
using Challenge.Web.API.Controllers;
using MyTested.AspNetCore.Mvc;

namespace Challenge.Tests.Tests;

public class FileUpload_WhenInvalidFilename
{
    public FileUpload_WhenInvalidFilename()
    {
        FilesystemHelper.DeleteFiles(SettingsHelper.GetStoragePath());
    }

    [Theory]
    [InlineData(@"../../somefile.txt")]
    [InlineData(@"../../otherfile.exe")]
    [InlineData(@"..\..\thirdfile.json")]
    [InlineData(@"../..\fourthfile.test")]
    [InlineData(@"../../../fifth")]
    [InlineData(@"..\..\..\sixth.exe")]
    [InlineData(@"%CommonProgramFiles%\seventh")]
    [InlineData(@"%PATH%/eight.bat")]
    [InlineData(@"%PATH%/dir/eight.bat")]
    [InlineData(@"~/../../nineth.sh")]
    [InlineData(@"~/tenth.sh")]
    [InlineData(@"/etc/something")]
    [InlineData(@"/etc/bin/something.sh")]
    [InlineData(@"/d/one/two.exe")]
    [InlineData(@"/c/one/three.exe")]
    [InlineData(@"\d\one\four.exe")]
    [InlineData(@"%cd%/file.one")]
    public void ShouldSanitizeFilename(string filename)
    {
        var request = FileUploadHelper.BuildXmlFileUploadRequestWithFilenames(filename);
        var response = FileUploadHelper.BuildExpectedResponse(request);

        MyMvc
            .Controller<FileController>()
            .Calling(x => x.Upload(request))
            .ShouldReturn()
            .Ok(response);

        var stripped = Path.GetFileName(filename);
        var dotIndex = stripped.IndexOf('.');
        var expectedFinaname = dotIndex == -1
            ? stripped + ".json"
            : $"{stripped[..dotIndex]}.json";

        Assert.True(FilesystemHelper.StorageFileExists(expectedFinaname));
    }
}
