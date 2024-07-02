using Challenge.Common.JSON;
using Challenge.Domain.Files;
using Challenge.Domain.Files.Abstractions;
using Challenge.Domain.Files.Objects;
using Challenge.Tests.Mocks;
using Challenge.Web.Common.Contracts;

namespace Challenge.Tests.Helpers;

internal static class FileUploadHelper
{
    private const string PLAIN_XML = @"<?xml version=""1.0"" encoding=""utf-8""?><Root>Content</Root>";

    public static FileUploadContract BuildXmlFileUploadRequest(params string[] files)
    {
        var list = new List<XmlEncodedFile>();
        foreach (var file in files)
        {
            var xmlPlainFile = new XmlPlainFile(file, FilesystemHelper.ReadTestInputFile(file));
            var xmlEncodedFile = (XmlEncodedFile)xmlPlainFile.Encode(new TestNotifier(), false);
            list.Add(xmlEncodedFile);
        }
        return new FileUploadContract(list);
    }

    public static FileUploadContract BuildXmlFileUploadRequestWithFilenames(params string[] filenames)
    {
        var xmlFiles = filenames
            .Select(x => new XmlPlainFile(x, PLAIN_XML))
            .Select(x => (XmlEncodedFile)x.Encode(new TestNotifier(), false));
        return new FileUploadContract(xmlFiles);
    }

    public static IEnumerable<IEncodedFile> BuildExpectedResponse(FileUploadContract request)
    {
        foreach (var xmlEncodedFile in request.Files)
        {
            var xmlPlainFile = xmlEncodedFile.Decode(new TestNotifier());
            var json = xmlPlainFile.Content.JsonFromXml();
            var jsonFile = new JsonFromXmlPlainFile(xmlPlainFile.Name, json);
            yield return jsonFile.Encode(new TestNotifier(), false);
        }
    }
}
