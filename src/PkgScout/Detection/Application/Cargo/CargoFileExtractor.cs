using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PkgScout.Detection.Application.Cargo.Models;

namespace PkgScout.Detection.Application.Cargo;

public sealed class CargoFileExtractor(ILogger<CargoFileExtractor> logger) : IFileExtractor<CargoFile>
{
    public IEnumerable<ApplicationPackage> Extract(CargoFile file)
    {
        try
        {
            logger.DetectionExtractFileStarted("Cargo", file.ScannedFile.Fullpath);
            var lines = File
                .ReadAllLines(file.ScannedFile.Fullpath)
                .Select(line => line.Trim())
                .ToList();

            var dependencies = new List<ApplicationPackage>();
            var isInDependenciesSection = false;
            const string dependencyPattern = @"^([\w-]+)\s*=\s*""([^""]+)""";

            foreach (var line in lines)
            {
                if (line.StartsWith('[') && !line.StartsWith("[dependencies]"))
                {
                    if (isInDependenciesSection) break;
                }

                if (line.Equals("[dependencies]"))
                {
                    isInDependenciesSection = true;
                    continue;
                }

                if (isInDependenciesSection)
                {
                    var match = Regex.Match(line, dependencyPattern);

                    if (match.Success)
                    {
                        dependencies.AddRange(new ApplicationPackage(
                            Name: match.Groups[1].Value,
                            Version: match.Groups[2].Value,
                            Project: file.ScannedFile.Fullpath,
                            Source: ApplicationPackageSource.Cargo
                        ));
                    }
                }
            }

            return dependencies;
        }
        catch (Exception exception)
        {
            logger.DetectionExtractFileFailed("Cargo", file.ScannedFile.Fullpath, exception);
            return [];
        }
    }
}