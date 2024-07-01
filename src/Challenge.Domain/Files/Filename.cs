using Challenge.Domain.Core;

namespace Challenge.Domain.Files;

public record Filename
{
    private const char DASH = '-';
    private const char DOT = '.';

    private readonly string? _filename;

    public Filename(string name, string extension)
    {
        if (name == null)
        {
            throw new DomainException("Filename is required");
        }
        var filename = Path.GetFileName(name);
        if (filename == string.Empty)
        {
            throw new DomainException($"Filename '{name}' is invalid");
        }

        filename = ReplaceInvalidCharacters(filename);
        filename = ReplaceExtension(filename, extension);

        _filename = filename;
    }

    private string ReplaceInvalidCharacters(string input)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        foreach (var invalid in invalidChars)
        {
            input = input.Replace(invalid, DASH);
        }
        return input;
    }

    private string ReplaceExtension(string file, string extension)
    {
        var dotIndex = file.IndexOf(DOT);
        file = dotIndex == -1
            ? file
            : file[..dotIndex];
        return $"{file}.{extension}";
    }

    public override string ToString()
    {
        return _filename ?? throw new Exception($"This is a bug: '{nameof(_filename)}' cannot be null");
    }

    public static implicit operator string(Filename filename)
    {
        return filename.ToString();
    }
}

public record JsonFilename : Filename
{
    public JsonFilename(string name) : base(name, "json")
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public static implicit operator string(JsonFilename filename)
    {
        return filename.ToString();
    }
}
