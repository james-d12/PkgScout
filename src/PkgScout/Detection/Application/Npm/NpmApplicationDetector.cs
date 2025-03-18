using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace PkgScout.Detection.Application.Npm;

public sealed class NpmApplicationDetector(
    ILogger<NpmApplicationDetector> logger,
    NpmFileMatcher npmFileMatcher,
    NpmFileExtractor npmFileExtractor) : IApplicationDetector
{
    public IEnumerable<ApplicationPackage> Detect(ImmutableList<ScannedFile> files)
    {
        try
        {
            DetectionLogTemplates.DetectionStarted(logger, "Npm");
            var matchedFiles = npmFileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                DetectionLogTemplates.FilesNotFound(logger, "Npm");
                return [];
            }

            DetectionLogTemplates.FilesMatched(logger, "Npm", matchedFiles.Count);
            return matchedFiles
                .SelectMany(npmFileExtractor.Extract);
        }
        catch (Exception exception)
        {
            DetectionLogTemplates.DetectionFailed(logger, "Npm", exception);
            return [];
        }
    }
}