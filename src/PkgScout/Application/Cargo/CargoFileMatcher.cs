using System.Collections.Immutable;
using PkgScout.Application.Cargo.Models;
using PkgScout.Application.Shared;

namespace PkgScout.Application.Cargo;

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