using Challenge.Common;
using Challenge.Domain.Files;

namespace Challenge.Tests.Models;

public record struct TestFileModel(string Name, string Content) : IPlainFile
{
    public IEncodedFile Encode(INotifier notifier)
    {
        throw new NotImplementedException();
    }
}
