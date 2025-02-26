namespace PkgScout.Modules.Applications.Shared;

public interface IService
{
    void Start(IReadOnlyList<string> files);
}