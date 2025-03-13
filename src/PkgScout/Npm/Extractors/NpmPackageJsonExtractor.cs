using System.Text.Json;
using PkgScout.Shared;

namespace PkgScout.Npm.Extractors;

public sealed class NpmPackageJsonExtractor : INpmExtractor
{
    public NpmFileType SupportedType => NpmFileType.PackageFile;

    public IEnumerable<Package> Extract(string content, NpmFile file)
    {
        var packageJson = JsonSerializer.Deserialize<NpmPackageJson>(content);

        if (packageJson is null)
        {
            return [];
        }

        var dependencies = new List<Package>();

        dependencies.AddRange(packageJson.Dependencies.Select(
            d => new Package(
                Name: d.Key,
                Version: d.Value,
                Project: file.ScannedFile.Fullpath,
                Source: PackageSource.Npm
            )));
        dependencies.AddRange(packageJson.DevDependencies.Select(
            d => new Package(
                Name: d.Key,
                Version: d.Value,
                Project: file.ScannedFile.Fullpath,
                Source: PackageSource.Npm
            )));

        return dependencies;
    }
}