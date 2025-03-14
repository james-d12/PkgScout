using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;

namespace PkgScout.Docker;

public sealed class DockerFileExtractor(ILogger<DockerFileExtractor> logger) : IFileExtractor<DockerFile>
{
    public IEnumerable<Package> Extract(DockerFile file)
    {
        logger.LogInformation("Extracting Packages from file: {File} {Type}",
            file.ScannedFile.Fullpath, file.FileType);

        var lines = File.ReadAllLines(file.ScannedFile.Fullpath);

        var packages = new List<Package>();

        foreach (var line in lines)
        {
            if (line.StartsWith("from", StringComparison.OrdinalIgnoreCase))
            {
                logger.LogDebug("Found FROM line in Docker file: {File}", file.ScannedFile.Fullpath);
                var trimmedLine = Regex.Replace(line, @"\s+", " ");
                var packageLine = trimmedLine.Split(" ").ElementAtOrDefault(1);

                if (packageLine is null) continue;

                var packageLineSplit = packageLine.Split(":");
                var packageName = packageLineSplit.ElementAtOrDefault(0);
                var packageVersion = packageLineSplit.ElementAtOrDefault(1) ?? string.Empty;

                if (packageName is null) continue;

                packages.Add(new Package
                {
                    Name = packageName,
                    Version = packageVersion,
                    Project = file.ScannedFile.Fullpath,
                    PackageSource = PackageSource.Docker
                });
            }
        }

        return packages;
    }
}