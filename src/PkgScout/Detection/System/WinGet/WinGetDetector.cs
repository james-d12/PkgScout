using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OsScout;
using PkgScout.Shared;

namespace PkgScout.Detection.System.WinGet;

public sealed class WinGetDetector(ILogger<WinGetDetector> logger) : ISystemDetector
{
    public HashSet<OperatingSystemType> SupportedOperatingSystems =>
    [
        OperatingSystemType.Windows
    ];

    public async Task<IEnumerable<SystemPackage>> DetectAsync()
    {
        try
        {
            logger.DetectionStarted("Winget");

            const string command = "winget";
            const string arguments = "list";

            var content = await CommandLine.ExecuteAndReturnStdOutAsync(command, arguments);

            var packages = new List<SystemPackage>();

            foreach (var line in content.Split("\n"))
            {
                if (string.IsNullOrEmpty(line)) continue;

                var trimmedLine = Regex.Replace(line, @"\s+", " ");

                var package = trimmedLine.Split(" ");

                var packageId = package.ElementAtOrDefault(1)?.Trim();
                var packageVersion = package.ElementAtOrDefault(2)?.Trim() ?? string.Empty;

                if (packageId is null) continue;

                packages.Add(new SystemPackage
                {
                    Name = packageId,
                    Version = packageVersion,
                    Source = SystemPackageSource.Winget
                });
            }

            return packages;
        }
        catch (Exception exception)
        {
            logger.DetectionFailed("Winget", exception);
            return [];
        }
    }
}