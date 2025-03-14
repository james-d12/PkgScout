using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Npm;

public sealed class NpmDetector(
    ILogger<NpmDetector> logger,
    NpmFileMatcher npmFileMatcher,
    NpmFileExtractor npmFileExtractor) : IDetector
{
    public IEnumerable<Package> Start(ImmutableList<ScannedFile> files)
    {
        try
        {
            logger.LogInformation("Starting Npm Detection");
            var matchedFiles = npmFileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                logger.LogWarning("Could not find files that matched Npm search criteria.");
                return [];
            }

            logger.LogInformation("Found: {Count} matched files for Npm.", matchedFiles.Count);

            return matchedFiles
                .SelectMany(npmFileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Could not detect Npm packages due to exception.");
            return [];
        }
    }
}