using System.Collections.Immutable;

namespace PkgScout.Shared.Filesystem;

public static class FileScanner
{
    public static ImmutableList<ScannedFile> GetFiles(string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException($"Directory {directory} does not exist");
        }

        return Directory
            .EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
            .Select(file => new ScannedFile(
                Filename: Path.GetFileName(file),
                Extension: Path.GetExtension(file),
                Fullpath: file))
            .ToImmutableList();
    }
}