namespace PkgScout.Core;

public interface IService
{
    void Start(IReadOnlyList<string> files);
}