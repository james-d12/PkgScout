using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OsScout;
using PkgScout.Shared;

namespace PkgScout.Detection.System.Dnf;

public sealed class DnfDetector(ILogger<DnfDetector> logger) : ISystemDetector
{
    public HashSet<OperatingSystemType> SupportedOperatingSystems =>
    [
        OperatingSystemType.Fedora,
        OperatingSystemType.AlmaLinux,
        OperatingSystemType.Rocky
    ];

    public async Task<IEnumerable<SystemPackage>> DetectAsync()
    {
        try
        {
            logger.DetectionStarted("Dnf");

            const string command = "dnf";
            const string arguments = "ls --installed";

            var content = await CommandLine.ExecuteAndReturnStdOutAsync(command, arguments);

            var lines = content.Split("\n");

            var packages = new List<SystemPackage>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var trimmedLine = Regex.Replace(line, @"\s+", " ");

                var package = trimmedLine.Split(" ");

                var packageName = package.ElementAtOrDefault(0)?.Trim();
                var packageVersion = package.ElementAtOrDefault(1)?.Trim() ?? string.Empty;

                if (packageName is null) continue;

                packages.Add(new SystemPackage
                {
                    Name = packageName,
                    Version = packageVersion,
                    Source = SystemPackageSource.Dnf
                });
            }

            return packages;
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("Dnf", exception);
            return [];
        }
    }
}