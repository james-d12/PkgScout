using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace PkgScout.Detection.Application.NuGet;

public sealed class NuGetApplicationDetector(
    ILogger<NuGetApplicationDetector> logger,
    NuGetFileMatcher fileMatcher,
    NuGetFileExtractor fileExtractor) : IApplicationDetector
{
    public IEnumerable<ApplicationPackage> Detect(ImmutableList<ScannedFile> files)
    {
        try
        {
            logger.DetectionStarted("NuGet");
            var matchedFiles = fileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                logger.DetectionFilesNotFound("NuGet");
                return [];
            }

            logger.DetectionFilesMatched("NuGet", matchedFiles.Count);
            return matchedFiles.SelectMany(fileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("NuGet", exception);
            return [];
        }
    }
}