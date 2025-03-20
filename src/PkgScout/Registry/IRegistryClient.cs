namespace PkgScout.Registry;

public interface IRegistryClient<T>
{
    Task<T> GetPackageInfoAsync(string name);
}