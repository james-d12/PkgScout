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
            logger.DetectionStarted("Docker");
            var matchedFiles = fileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                logger.FilesNotFound("Docker");
                return [];
            }

            logger.FilesMatched("Docker", matchedFiles.Count);
            return matchedFiles.SelectMany(fileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("Docker", exception);
            return [];
        }
    }
}