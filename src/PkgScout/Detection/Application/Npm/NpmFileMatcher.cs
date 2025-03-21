using System.Collections.Immutable;
using PkgScout.Detection.Application.Npm.Models;

namespace PkgScout.Detection.Application.Npm;

public sealed class NpmFileMatcher : IFileMatcher<NpmFile>
{
    private static readonly Dictionary<string, NpmFileType> FileNamePatterns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "package.json", NpmFileType.PackageFile },
            { "package-lock.json", NpmFileType.PackageLockFile },
            { "npm-shrinkwrap.json", NpmFileType.PackageLockFile },
        };

    public ImmutableList<NpmFile> GetMatches(ImmutableList<ScannedFile> files)
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