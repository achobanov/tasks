using Challenge.Domain;

namespace Challenge.Common.Contravts;

public record FileUploadContract(IEnumerable<FileModel> Files);
