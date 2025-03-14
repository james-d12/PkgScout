using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;
using PkgScout.Shared.Logging;

namespace PkgScout.Cargo;

public sealed class CargoDetector(
    ILogger<CargoDetector> logger,
    CargoFileMatcher cargoFileMatcher,
    CargoFileExtractor cargoFileExtractor)
    : IDetector
{
    public IEnumerable<Package> Detect(ImmutableList<ScannedFile> files)
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