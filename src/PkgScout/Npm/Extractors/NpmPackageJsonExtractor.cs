using System.Text.Json;
using PkgScout.Shared;

namespace PkgScout.Npm.Extractors;

public sealed class NpmPackageJsonExtractor : INpmExtractor
{
    public NpmFileType SupportedType => NpmFileType.PackageFile;

    public IEnumerable<Package> Extract(string content)
    {
        var packageJson = JsonSerializer.Deserialize<NpmPackageJson>(content);

        if (packageJson is null)
        {
            return [];
        }

        var dependencies = new List<Package>();

        dependencies.AddRange(packageJson.Dependencies.Select(
            d => new Package(d.Key, d.Value)));
        dependencies.AddRange(packageJson.DevDependencies.Select(
            d => new Package(d.Key, d.Value)));

        return dependencies;
    }
}