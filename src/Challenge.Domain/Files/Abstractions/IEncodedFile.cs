using Challenge.Common;
namespace Challenge.Domain.Files.Abstractions;

public interface IEncodedFile : IFile
{
    string EncodingName { get; }
    IPlainFile Decode(INotifier notifier, bool validate = true);
}

