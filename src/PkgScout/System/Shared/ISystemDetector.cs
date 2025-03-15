using OsScout;

namespace PkgScout.System.Shared;

public interface ISystemDetector
{
    HashSet<OperatingSystemType> SupportedOperatingSystems { get; }

    Task<IEnumerable<SystemPackage>> DetectAsync();
}