using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Npm;

public sealed class NpmDetector(
    ILogger<NpmDetector> logger,
    NpmFileMatcher npmFileMatcher,
    NpmPackageExtractor npmPackageExtractor) : IDetector
{
    public IEnumerable<Package> Start(ImmutableList<ScannedFile> files)
    {
        logger.LogInformation("Starting Npm Detection");
        var matchedFiles = npmFileMatcher.GetMatches(files);

        if (matchedFiles.Count == 0)
        {
            logger.LogWarning("Could not find files that matched Npm search criteria.");
            return [];
        }

        return matchedFiles
            .SelectMany(npmPackageExtractor.Extract);
    }
}