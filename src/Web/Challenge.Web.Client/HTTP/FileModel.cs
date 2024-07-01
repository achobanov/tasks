using Challenge.Domain.Files.Abstractions;

namespace Challenge.Web.Client.HTTP;

public readonly record struct FileModel(string Name, string Content) : IFile;
