using System.Text.Json;
using PkgScout.Npm.Models;
using PkgScout.Shared;

namespace PkgScout.Npm.Extractors;

public sealed class NpmPackageLockJsonExtractor : INpmExtractor
{
    public NpmFileType SupportedType => NpmFileType.PackageLockFile;

    public IEnumerable<Package> Extract(string content, NpmFile file)
    {
        var packageJson = JsonSerializer.Deserialize<NpmPackageLockJson>(content);

        if (packageJson is null)
        {
            return [];
        }

        var dependencies = new List<Package>();

        dependencies.AddRange(packageJson.Dependencies.Values.Select(
            d => new Package(
                Name: d.Resolved,
                Version: d.Version,
                Project: file.ScannedFile.Filename,
                PackageSource: PackageSource.Npm
            )));
        dependencies.AddRange(packageJson.Packages.Values.Select(
            d => new Package(
                Name: d.Resolved,
                Version: d.Version,
                Project: file.ScannedFile.Filename,
                PackageSource: PackageSource.Npm
            )));

        return dependencies;
    }
}