using System.Collections.Immutable;

namespace PkgScout.Application.Shared;

public interface IApplicationDetector
{
    IEnumerable<ApplicationPackage> Detect(ImmutableList<ScannedFile> files);
}