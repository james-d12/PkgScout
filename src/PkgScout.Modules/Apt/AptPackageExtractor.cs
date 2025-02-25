using PkgScout.Core;

namespace PkgScout.Modules.Apt;

public sealed class AptPackageExtractor : IPackageExtractor
{
    public IEnumerable<Package> Extract(string file)
    {
        throw new NotImplementedException();
    }
}