using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace PkgScout.Detection.Application.Cargo;

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
            DetectionLogTemplates.DetectionStarted(logger, "Cargo");
            var matchedFiles = cargoFileMatcher.GetMatches(files);

            if (matchedFiles.Count == 0)
            {
                DetectionLogTemplates.FilesNotFound(logger, "Cargo");
                return [];
            }

            DetectionLogTemplates.FilesMatched(logger, "Cargo", matchedFiles.Count);
            return matchedFiles.SelectMany(cargoFileExtractor.Extract);
        }
        catch (Exception exception)
        {
            DetectionLogTemplates.DetectionFailed(logger, "Cargo", exception);
            return [];
        }
    }
}