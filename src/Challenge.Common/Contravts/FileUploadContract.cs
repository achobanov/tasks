namespace Challenge.Common.Contravts;

public record FileUploadContract(IEnumerable<FileModel> Files);

public record struct FileModel(string Filename, string Content);
