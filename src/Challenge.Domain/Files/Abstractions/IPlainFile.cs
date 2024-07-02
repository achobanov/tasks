using Challenge.Common;

namespace Challenge.Domain.Files.Abstractions;

public interface IPlainFile : IFile
{
    IEncodedFile Encode(INotifier notifier, bool validate = true);
}