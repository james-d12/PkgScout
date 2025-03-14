using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;

namespace PkgScout.Cargo;

public sealed class CargoFileExtractor : IFileExtractor<CargoFile>
{
    private readonly ILogger<CargoFileExtractor> _logger;

    public CargoFileExtractor(ILogger<CargoFileExtractor> logger)
    {
        _logger = logger;
    }

    public IEnumerable<Package> Extract(CargoFile file)
    {
        try
        {
            _logger.LogInformation("Extracting Packages from file: {File}", file.ScannedFile.Fullpath);
            var lines = File
                .ReadAllLines(file.ScannedFile.Fullpath)
                .Select(line => line.Trim())
                .ToList();

            var dependencies = new List<Package>();
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
                        dependencies.AddRange(new Package(
                            Name: match.Groups[1].Value,
                            Version: match.Groups[2].Value,
                            Project: file.ScannedFile.Fullpath,
                            PackageSource: PackageSource.Cargo
                        ));
                    }
                }
            }

            return dependencies;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error extracting Npm package.");
            return [];
        }
    }
}