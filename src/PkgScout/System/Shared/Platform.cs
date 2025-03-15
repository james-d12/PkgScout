using OsScout;

namespace PkgScout.System.Shared;

public sealed class Platform
{
    private readonly Dictionary<HashSet<OperatingSystemType>, ISystemDetector> _detectors;

    public Platform(IEnumerable<ISystemDetector> systemDetectors)
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