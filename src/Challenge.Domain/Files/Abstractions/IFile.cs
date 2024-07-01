namespace Challenge.Domain.Files.Abstractions;

public interface IFile
{
    string Name { get; }
    string Content { get; }
}
