using Challenge.Common;
using System.Text;

namespace Challenge.Domain;

public static class EncodingProvider
{
    public static Encoding GetEncodingOrUtf8(INotifier notifier, string encodingName)
    {
        try
        {
            return Encoding.GetEncoding(encodingName);
        }
        catch (ArgumentException)
        {
            notifier.Information($"Encoding '{encodingName}' is not supported. Defaulting to UTF8");
            return Encoding.UTF8;
        }
    }
}
