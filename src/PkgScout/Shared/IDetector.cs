using System.Collections.Immutable;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Shared;

public interface IDetector
{
    IEnumerable<Package> Detect(ImmutableList<ScannedFile> files);
}