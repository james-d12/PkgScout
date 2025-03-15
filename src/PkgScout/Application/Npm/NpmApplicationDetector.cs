using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Application.Shared;
using PkgScout.Shared;

namespace PkgScout.Application.Npm;

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