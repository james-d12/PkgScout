using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Cargo;

public sealed class CargoDetector(
    ILogger<CargoDetector> logger,
    CargoFileMatcher cargoFileMatcher,
    CargoFileExtractor cargoFileExtractor)
    : IDetector
{
    public IEnumerable<Package> Start(ImmutableList<ScannedFile> files)
    {
        logger.LogInformation("Starting Cargo Detection");
        var matchedFiles = cargoFileMatcher.GetMatches(files);

        if (matchedFiles.Count == 0)
        {
            logger.LogWarning("Could not find files that matched Cargo search criteria.");
            return [];
        }

        return matchedFiles.SelectMany(cargoFileExtractor.Extract);
    }
}