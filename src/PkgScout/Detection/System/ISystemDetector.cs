using OsScout;

namespace PkgScout.Detection.System;

public interface ISystemDetector
{
    HashSet<OperatingSystemType> SupportedOperatingSystems { get; }

    Task<IEnumerable<SystemPackage>> DetectAsync();
}