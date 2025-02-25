namespace PkgScout.Core;

public interface IPackageExtractor
{
    IEnumerable<Package> Extract(string file);
}