using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OsScout;

namespace PkgScout.Detection.System.Pacman;

public sealed class PacmanDetector(ILogger<PacmanDetector> logger) : ISystemDetector
{
    public HashSet<OperatingSystemType> SupportedOperatingSystems =>
    [
        OperatingSystemType.Arch,
        OperatingSystemType.Arch32,
        OperatingSystemType.Antergos,
        OperatingSystemType.ArcoLinux,
        OperatingSystemType.BlackArch,
        OperatingSystemType.Hyperbola,
        OperatingSystemType.RebornOs,
        OperatingSystemType.Garuda,
        OperatingSystemType.Artix,
        OperatingSystemType.EndeavourOs,
        OperatingSystemType.Manjaro
    ];

    public async Task<IEnumerable<SystemPackage>> DetectAsync()
    {
        try
        {
            logger.DetectionStarted("Pacman");

            const string command = "pacman";
            const string arguments = "-Q";

            var content = await CommandLine.Execute(command, arguments);

            var packages = new List<SystemPackage>();

            foreach (var line in content.Split("\n"))
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
                    Source = SystemPackageSource.Pacman
                });
            }

            return packages;
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("Pacman", exception);
            return [];
        }
    }
}