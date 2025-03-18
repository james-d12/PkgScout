using System.Collections.Immutable;
using PkgScout.Detection.Application.Cargo.Models;

namespace PkgScout.Detection.Application.Cargo;

public sealed class CargoFileMatcher : IFileMatcher<CargoFile>
{
    public ImmutableList<CargoFile> GetMatches(ImmutableList<ScannedFile> files)
    {
        return files
            .Where(file => file.Filename.Equals("Cargo.toml", StringComparison.OrdinalIgnoreCase))
            .Select(file => new CargoFile(file))
            .ToImmutableList();
    }
}