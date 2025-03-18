using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace PkgScout.Detection.Application.Docker;

public sealed class DockerApplicationDetector(
    ILogger<DockerApplicationDetector> logger,
    DockerFileMatcher fileMatcher,
    DockerFileExtractor fileExtractor) : IApplicationDetector
{
    public IEnumerable<ApplicationPackage> Detect(ImmutableList<ScannedFile> files)
    {
        try
        {
            DetectionLogTemplates.DetectionStarted(logger, "Docker");
            var matchedFiles = fileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                DetectionLogTemplates.FilesNotFound(logger, "Docker");
                return [];
            }

            DetectionLogTemplates.FilesMatched(logger, "Docker", matchedFiles.Count);
            return matchedFiles.SelectMany(fileExtractor.Extract);
        }
        catch (Exception exception)
        {
            DetectionLogTemplates.DetectionFailed(logger, "Docker", exception);
            return [];
        }
    }
}