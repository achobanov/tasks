using Challenge.Domain.Files.Objects;

namespace Challenge.Web.Common.Contracts;

public record FileUploadContract(IEnumerable<XmlEncodedFile> Files);