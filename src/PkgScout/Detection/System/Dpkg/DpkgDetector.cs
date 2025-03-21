using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OsScout;
using PkgScout.Shared;

namespace PkgScout.Detection.System.Dpkg;

public sealed class DpkgDetector(ILogger<DpkgDetector> logger) : ISystemDetector
{
    public HashSet<OperatingSystemType> SupportedOperatingSystems =>
    [
        OperatingSystemType.Debian,
        OperatingSystemType.Devuan,
        OperatingSystemType.Elementary,
        OperatingSystemType.LinuxMint,
        OperatingSystemType.PopOs,
        OperatingSystemType.Pureos,
        OperatingSystemType.Raspbian,
        OperatingSystemType.Ubuntu,
        OperatingSystemType.Deepin,
        OperatingSystemType.Trisquel,
        OperatingSystemType.Kali,
        OperatingSystemType.Parrot
    ];

    public async Task<IEnumerable<SystemPackage>> DetectAsync()
    {
        try
        {
            logger.DetectionStarted("Dpkg");

            const string command = "dpkg-query";
            const string arguments = "-W";

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
                    Source = SystemPackageSource.Dpkg
                });
            }

            return packages;
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("Dpkg", exception);
            return [];
        }
    }
}