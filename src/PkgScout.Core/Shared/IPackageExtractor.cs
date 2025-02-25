namespace PkgScout.Core.Shared;

public interface IPackageExtractor
{
    IEnumerable<Package> Extract(string file);
}