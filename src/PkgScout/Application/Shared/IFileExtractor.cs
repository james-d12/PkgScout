namespace PkgScout.Application.Shared;

public interface IFileExtractor<in T>
{
    IEnumerable<ApplicationPackage> Extract(T file);
}