using Challenge.Domain.Files;
using Challenge.Domain.Files.Abstractions;
using Challenge.Domain.Files.Objects;
using Challenge.Tests.Models;
using Challenge.Web.API.Controllers;
using Challenge.Web.API.Logging;
using Challenge.Web.Common.Contracts;
using Moq;
using MyTested.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;

namespace Challenge.Tests.Tests;

public class FileUpload_Success
{
    [Fact]
    public void Test()
    {
        var filename = "name.json";
        var xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?><Test attr=""test-attr"">Content</Test>";
        var xmlFile = new XmlPlainFile(filename, xmlContent);
        var encodedXmlFile = (XmlEncodedFile)xmlFile.Encode(new Logger());
        var contract = new FileUploadContract([encodedXmlFile]);
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlContent);

        var jsonContent = JsonConvert.SerializeXmlNode(xmlDocument, Newtonsoft.Json.Formatting.Indented);
        var resultFile = new JsonFromXmlPlainFile(filename, jsonContent);
        var expectedResult = new List<IPlainFile> { resultFile };

        MyMvc
            .Controller<FileController>()
            .Calling(x => x.Upload(contract))
            .ShouldReturn()
            .Ok(expectedResult);
    }
}
