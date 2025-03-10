using System.Collections.Immutable;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Cargo;

public sealed class CargoFileMatcher
{
    public ImmutableList<CargoFile> GetMatches(ImmutableList<ScannedFile> files)
    {
        return files
            .Where(file => file.Filename.Equals("Cargo.toml", StringComparison.OrdinalIgnoreCase))
            .Select(file => new CargoFile(file))
            .ToImmutableList();
    }
}