using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Applications.Npm;

public sealed class NpmService(
    NpmFileMatcher npmFileMatcher,
    NpmPackageExtractor npmPackageExtractor) : IService
{
    public void Start(IReadOnlyList<string> files)
    {
        var matchedFiles = npmFileMatcher.GetMatches(files);

        if (!matchedFiles.Files.Any())
        {
            return;
        }

        foreach (var file in matchedFiles.Files)
        {
            var npmPackages = npmPackageExtractor.Extract(file);
            Helper.PrintPackages(file, npmPackages);
        }
    }
}