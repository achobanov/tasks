using Challenge.Common;
using Challenge.Domain.Core;
using System.Text;

namespace Challenge.Domain.Files;

public static class FilesizeValidator
{
    private static readonly Filesize _limit = new(Constants.MegaByte);

    public static string Base64Decode(this IEncodedFile file, INotifier notifier, bool validate = true)
    {
        var encoding = EncodingProvider.GetEncodingOrUtf8(notifier, file.EncodingName);
        return Base64Decode(file, encoding);
    }

    public static string Base64Encode(this IPlainFile file, Encoding encoding, bool validate = true)
    {
        var bytes = encoding.GetBytes(file.Content);
        if (validate)
        {
            Validate(bytes, file.Name);
        }

        return Convert.ToBase64String(bytes);
    }

    public static string Base64Decode(this IEncodedFile file, Encoding encoding, bool validate = true)
    {
        var bytes = Convert.FromBase64String(file.Content);
        if (validate)
        {
            Validate(bytes, file.Name);
        }

        return encoding.GetString(bytes);
    }

    public static void Validate(byte[] bytes, string name)
    {
        var size = new Filesize(bytes.Length);
        if (size > _limit)
        {
            throw new DomainException($"File '{name}' is too large: '{size}', Maximum file size is '{_limit}'");
        }
    }
}
