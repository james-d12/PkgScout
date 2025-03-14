namespace PkgScout.Shared;

public interface IFileExtractor<in T>
{
    IEnumerable<Package> Extract(T file);
}