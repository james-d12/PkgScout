using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.CommandLine;
using PkgScout.Shared.Logging;

namespace PkgScout.Apt;

public sealed class AptDetector(ILogger<AptDetector> logger) : ISystemDetector
{
    public async Task<IEnumerable<Package>> DetectAsync()
    {
        try
        {
            logger.DetectionStarted("Apt");

            const string command = "dpkg-query";
            const string arguments = "-W";

            var content = await CommandLine.Execute(command, arguments, logger);

            var lines = content.Split("\n");

            var packages = new List<Package>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var trimmedLine = Regex.Replace(line, @"\s+", " ");

                var package = trimmedLine.Split(" ");

                var packageName = package.ElementAtOrDefault(0)?.Trim();
                var packageVersion = package.ElementAtOrDefault(1)?.Trim() ?? string.Empty;

                if (packageName is null) continue;

                packages.Add(new Package
                {
                    Name = packageName,
                    Version = packageVersion,
                    Project = "System",
                    PackageSource = PackageSource.Apt
                });
            }

            return packages;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}