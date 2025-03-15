using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Application.Shared;
using PkgScout.Shared;

namespace PkgScout.Application.Cargo;

public sealed class CargoApplicationDetector(
    ILogger<CargoApplicationDetector> logger,
    CargoFileMatcher cargoFileMatcher,
    CargoFileExtractor cargoFileExtractor)
    : IApplicationDetector
{
    public IEnumerable<ApplicationPackage> Detect(ImmutableList<ScannedFile> files)
    {
        try
        {
            logger.DetectionStarted("Cargo");
            var matchedFiles = cargoFileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                logger.FilesNotFound("Cargo");
                return [];
            }

            logger.FilesMatched("Cargo", matchedFiles.Count);
            return matchedFiles.SelectMany(cargoFileExtractor.Extract);
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("Cargo", exception);
            return [];
        }
    }
}