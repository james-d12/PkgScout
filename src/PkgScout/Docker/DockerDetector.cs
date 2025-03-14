using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;
using PkgScout.Shared.Logging;

namespace PkgScout.Docker;

public sealed class DockerDetector(
    ILogger<DockerDetector> logger,
    DockerFileMatcher fileMatcher,
    DockerFileExtractor fileExtractor) : IDetector
{
    public IEnumerable<Package> Detect(ImmutableList<ScannedFile> files)
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