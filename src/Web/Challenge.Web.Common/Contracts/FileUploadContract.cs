using Challenge.Domain.Files;

namespace Challenge.Web.Common.Contracts;

public record FileUploadContract(IEnumerable<XmlEncodedFile> Files);