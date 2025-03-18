using System.Collections.Immutable;

namespace PkgScout.Detection.Application;

public interface IApplicationDetector
{
    IEnumerable<ApplicationPackage> Detect(ImmutableList<ScannedFile> files);
}