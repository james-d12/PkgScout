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
            DetectionLogTemplates.DetectionStarted(logger, "NuGet");
            var matchedFiles = fileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                DetectionLogTemplates.FilesNotFound(logger, "NuGet");
                return [];
            }

            DetectionLogTemplates.FilesMatched(logger, "NuGet", matchedFiles.Count);
            return matchedFiles.SelectMany(fileExtractor.Extract);
        }
        catch (Exception exception)
        {
            DetectionLogTemplates.DetectionFailed(logger, "NuGet", exception);
            return [];
        }
    }
}