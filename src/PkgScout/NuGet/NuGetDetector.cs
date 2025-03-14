using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;
using PkgScout.Shared.Logging;

namespace PkgScout.NuGet;

public sealed class NuGetDetector(
    ILogger<NuGetDetector> logger,
    NuGetFileMatcher fileMatcher,
    NuGetFileExtractor fileExtractor) : IDetector
{
    public IEnumerable<Package> Detect(ImmutableList<ScannedFile> files)
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