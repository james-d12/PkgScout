using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Applications.NuGet;

public sealed class NugetFileMatcher : IFileMatcher
{
    public MatchedFiles GetMatches(IReadOnlyList<string> files)
    {
        var matchedFiles = files.Where(file =>
            file.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase) ||
            file.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) ||
            file.EndsWith("packages.config", StringComparison.OrdinalIgnoreCase) ||
            file.EndsWith("Directory.Packages.Props", StringComparison.OrdinalIgnoreCase));

        return new MatchedFiles(matchedFiles, MatchedFileType.NuGet);
    }
}