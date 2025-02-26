using System.Text.Json;
using Microsoft.Extensions.Logging;
using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Applications.Npm;

public sealed class NpmPackageExtractor(ILogger<NpmPackageExtractor> logger) : IPackageExtractor
{
    public IEnumerable<Package> Extract(string file)
    {
        try
        {
            var content = File.ReadAllText(file);
            var packageJson = JsonSerializer.Deserialize<NpmPackageJson>(content);

            if (packageJson is null)
            {
                logger.LogInformation("File: {File} was not a valid Npm file.", file);
                return [];
            }

            var dependencies = new List<Package>();
            dependencies.AddRange(packageJson.Dependencies.Select(
                d => new Package(d.Key, d.Value)));
            dependencies.AddRange(packageJson.DevDependencies.Select(
                d => new Package(d.Key, d.Value)));

            return dependencies;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error extracting Npm package.");
            return [];
        }
    }
}