namespace PkgScout.Detection.Application;

public interface IFileExtractor<in T>
{
    IEnumerable<ApplicationPackage> Extract(T file);
}