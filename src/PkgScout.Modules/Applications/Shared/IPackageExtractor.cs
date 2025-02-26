namespace PkgScout.Modules.Applications.Shared;

public interface IPackageExtractor
{
    IEnumerable<Package> Extract(string file);
}