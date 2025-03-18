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
            logger.DetectionStarted("Npm");
            var matchedFiles = npmFileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                logger.DetectionFilesNotFound("Npm");
                return [];
            }

            logger.DetectionFilesMatched("Npm", matchedFiles.Count);
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