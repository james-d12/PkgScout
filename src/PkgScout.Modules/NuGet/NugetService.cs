using PkgScout.Core;

namespace PkgScout.Modules.NuGet;

public sealed class NugetService(
    NugetFileMatcher nugetFileMatcher,
    NugetPackageExtractor nugetPackageExtractor)
    : IService
{
    public void Start(IReadOnlyList<string> files)
    {
        var matchedFiles = nugetFileMatcher.GetMatches(files);

        if (!matchedFiles.Files.Any())
        {
            return;
        }

        foreach (var file in matchedFiles.Files)
        {
            var nugetPackages = nugetPackageExtractor.Extract(file);
            Helper.PrintPackages(file, nugetPackages);
        }
    }
}