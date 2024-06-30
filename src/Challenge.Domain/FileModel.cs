using System.Text;

namespace Challenge.Domain;

public record struct FileModel(string Filename, string Content, string EncodingName);