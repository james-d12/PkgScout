using PkgScout.Core.Shared;

namespace PkgScout.Core.Modules.NuGet;

public sealed class NugetFileMatcher : IFileMatcher
{
    public MatchedFiles GetMatches(IReadOnlyList<string> files)
    {
        var matchedFiles = files.Where(file =>
            file.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase) ||
            file.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase) ||
            file.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) ||
            file.Equals("packages.config", StringComparison.OrdinalIgnoreCase) ||
            file.Equals("Directory.Packages.Props", StringComparison.OrdinalIgnoreCase));

        return new MatchedFiles(matchedFiles, MatchedFileType.NuGet);
    }
}