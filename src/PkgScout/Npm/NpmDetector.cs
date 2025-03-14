using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;
using PkgScout.Shared.Logging;

namespace PkgScout.Npm;

public sealed class NpmDetector(
    ILogger<NpmDetector> logger,
    NpmFileMatcher npmFileMatcher,
    NpmFileExtractor npmFileExtractor) : IDetector
{
    public IEnumerable<Package> Start(ImmutableList<ScannedFile> files)
    {
        try
        {
            logger.DetectionStarted("Npm");
            var matchedFiles = npmFileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                logger.FilesNotFound("Npm");
                return [];
            }

            logger.FilesMatched("Npm", matchedFiles.Count);
            return matchedFiles
                .SelectMany(npmFileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("Npm", exception);
            return [];
        }
    }
}