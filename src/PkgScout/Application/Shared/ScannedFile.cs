namespace PkgScout.Application.Shared;

public readonly record struct ScannedFile(string Filename, string Extension, string Fullpath)
{
    public static ScannedFile Create(string fullpath)
    {
        ArgumentException.ThrowIfNullOrEmpty(fullpath);
        var filename = Path.GetFileName(fullpath);
        var extension = Path.GetExtension(fullpath);
        return new ScannedFile(filename, extension, fullpath);
    }
}