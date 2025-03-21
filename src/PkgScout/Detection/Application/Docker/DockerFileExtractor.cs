using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PkgScout.Detection.Application.Docker.Models;

namespace PkgScout.Detection.Application.Docker;

public sealed class DockerFileExtractor(ILogger<DockerFileExtractor> logger) : IFileExtractor<DockerFile>
{
    public IEnumerable<ApplicationPackage> Extract(DockerFile file)
    {
        try
        {
            logger.DetectionExtractFileStarted("Docker", file.ScannedFile.Fullpath);

            var lines = File.ReadAllLines(file.ScannedFile.Fullpath);

            var packages = new List<ApplicationPackage>();

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

                    packages.Add(new ApplicationPackage
                    {
                        Name = packageName,
                        Version = packageVersion,
                        Project = file.ScannedFile.Fullpath,
                        Source = ApplicationPackageSource.Docker
                    });
                }
            }

            return packages;
        }
        catch (Exception exception)
        {
            logger.DetectionExtractFileFailed("Docker", file.ScannedFile.Fullpath, exception);
            return [];
        }
    }
}