using System.Collections.Immutable;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Shared;

public interface IDetector
{
    IEnumerable<Package> Start(ImmutableList<ScannedFile> files);
}