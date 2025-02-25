using PkgScout.Core.Shared;

namespace PkgScout.Core.Modules.Apt;

public sealed class AptPackageExtractor : IPackageExtractor
{
    public IEnumerable<Package> Extract(string file)
    {
        throw new NotImplementedException();
    }
}