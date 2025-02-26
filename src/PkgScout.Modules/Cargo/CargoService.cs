using PkgScout.Core;

namespace PkgScout.Modules.Cargo;

public sealed class CargoService(
    CargoFileMatcher cargoFileMatcher,
    CargoPackageExtractor cargoPackageExtractor)
    : IService
{
    public void Start(IReadOnlyList<string> files)
    {
        var matchedFiles = cargoFileMatcher.GetMatches(files);

        if (!matchedFiles.Files.Any())
        {
            return;
        }

        foreach (var file in matchedFiles.Files)
        {
            var cargoPackages = cargoPackageExtractor.Extract(file);
            Helper.PrintPackages(file, cargoPackages);
        }
    }
}