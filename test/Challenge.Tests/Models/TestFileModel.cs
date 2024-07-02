using Challenge.Common;
using Challenge.Domain.Files.Abstractions;

namespace Challenge.Tests.Models;

public record struct TestFileModel(string Name, string Content) : IPlainFile
{
    public IEncodedFile Encode(INotifier notifier, bool validate = false)
    {
        throw new NotImplementedException();
    }
}
