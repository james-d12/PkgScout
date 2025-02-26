using System.Collections.Immutable;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Npm;

public sealed class NpmFileMatcher
{
    public ImmutableList<NpmFile> GetMatches(IReadOnlyList<ScannedFile> files)
    {
        return files
            .Where(file => file.Filename.Equals("package.json", StringComparison.OrdinalIgnoreCase))
            .Select(file => new NpmFile(file))
            .ToImmutableList();
    }
}