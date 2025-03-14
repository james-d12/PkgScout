using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;

namespace PkgScout.NuGet;

public sealed class NuGetDetector(
    ILogger<NuGetDetector> logger,
    NuGetFileMatcher fileMatcher,
    NuGetFileExtractor fileExtractor) : IDetector
{
    public IEnumerable<Package> Start(ImmutableList<ScannedFile> files)
    {
        try
        {
            logger.LogInformation("Starting NuGet Detection");
            var matchedFiles = fileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                logger.LogWarning("Could not find files that matched NuGet search criteria.");
                return [];
            }

            logger.LogInformation("Found: {Count} matched files for NuGet.", matchedFiles.Count);
            return matchedFiles.SelectMany(fileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Could not detect NuGet packages due to exception.");
            return [];
        }
    }
}