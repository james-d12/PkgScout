using PkgScout.Core;

namespace PkgScout.Modules.Npm;

public sealed class NpmFileMatcher : IFileMatcher
{
    public MatchedFiles GetMatches(IReadOnlyList<string> files)
    {
        var matchedFiles = files.Where(file =>
            file.Equals("package.json", StringComparison.OrdinalIgnoreCase));
        return new MatchedFiles(matchedFiles, MatchedFileType.Npm);
    }
}