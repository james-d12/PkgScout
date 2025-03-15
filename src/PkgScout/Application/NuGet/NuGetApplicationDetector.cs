using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Application.Shared;
using PkgScout.Shared;

namespace PkgScout.Application.NuGet;

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
                logger.FilesNotFound("NuGet");
                return [];
            }

            logger.FilesMatched("NuGet", matchedFiles.Count);
            return matchedFiles.SelectMany(fileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("NuGet", exception);
            return [];
        }
    }
}