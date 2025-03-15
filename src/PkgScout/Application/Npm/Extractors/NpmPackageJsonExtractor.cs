using System.Text.Json;
using PkgScout.Application.Npm.Models;
using PkgScout.Application.Shared;

namespace PkgScout.Application.Npm.Extractors;

public sealed class NpmPackageJsonExtractor : INpmExtractor
{
    public NpmFileType SupportedType => NpmFileType.PackageFile;

    public IEnumerable<ApplicationPackage> Extract(string content, NpmFile file)
    {
        var packageJson = JsonSerializer.Deserialize<NpmPackageJson>(content);

        if (packageJson is null)
        {
            return [];
        }

        var dependencies = new List<ApplicationPackage>();

        dependencies.AddRange(packageJson.Dependencies.Select(
            d => new ApplicationPackage(
                Name: d.Key,
                Version: d.Value,
                Project: file.ScannedFile.Fullpath,
                Source: ApplicationPackageSource.Npm
            )));
        dependencies.AddRange(packageJson.DevDependencies.Select(
            d => new ApplicationPackage(
                Name: d.Key,
                Version: d.Value,
                Project: file.ScannedFile.Fullpath,
                Source: ApplicationPackageSource.Npm
            )));

        return dependencies;
    }
}