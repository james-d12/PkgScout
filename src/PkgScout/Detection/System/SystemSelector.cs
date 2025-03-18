using OsScout;

namespace PkgScout.Detection.System;

public sealed class SystemSelector
{
    private readonly Dictionary<HashSet<OperatingSystemType>, ISystemDetector> _detectors;

    public SystemSelector(IEnumerable<ISystemDetector> systemDetectors)
    {
        _detectors = systemDetectors.ToDictionary(systemDetector => systemDetector.SupportedOperatingSystems);
    }

    public ISystemDetector? GetSystemDetector()
    {
        var platform = OperatingSystemInfo.GetOperatingSystem();

        return _detectors
            .Select(detector => detector)
            .FirstOrDefault(detector => detector.Key.Contains(platform))
            .Value;
    }
}