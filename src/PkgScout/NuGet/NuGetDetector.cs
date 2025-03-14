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
            var matches = fileMatcher.GetMatches(files);

            if (matches.Count == 0)
            {
                logger.LogWarning("Could not find files that matched NuGet search criteria.");
                return [];
            }

            return matches
                .SelectMany(fileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Could not detect NuGet packages due to exception.");
            return [];
        }
    }
}