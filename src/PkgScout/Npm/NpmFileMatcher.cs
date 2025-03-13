using System.Collections.Immutable;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Npm;

public sealed class NpmFileMatcher
{
    private static readonly Dictionary<string, NpmFileType> FileNamePatterns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "packages.json", NpmFileType.PackageFile },
            { "packages-lock.json", NpmFileType.PackageLockFile },
            { "npm-shrinkwrap.json", NpmFileType.ShrinkWrapFIle },
        };

    public ImmutableList<NpmFile> GetMatches(IReadOnlyList<ScannedFile> files)
    {
        return files
            .Where(file => FileNamePatterns.ContainsKey(file.Filename))
            .Select(file =>
            {
                var fileType = FileNamePatterns[file.Filename];
                return new NpmFile(file, fileType);
            })
            .ToImmutableList();
    }
}