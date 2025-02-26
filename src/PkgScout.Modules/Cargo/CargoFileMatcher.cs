using PkgScout.Core;

namespace PkgScout.Modules.Cargo;

public sealed class CargoFileMatcher : IFileMatcher
{
    public MatchedFiles GetMatches(IReadOnlyList<string> files)
    {
        var matchedFiles = files.Where(file =>
            file.EndsWith("Cargo.toml", StringComparison.OrdinalIgnoreCase));
        return new MatchedFiles(matchedFiles, MatchedFileType.Cargo);
    }
}