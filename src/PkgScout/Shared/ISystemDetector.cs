namespace PkgScout.Shared;

public interface ISystemDetector
{
    Task<IEnumerable<Package>> DetectAsync();
}