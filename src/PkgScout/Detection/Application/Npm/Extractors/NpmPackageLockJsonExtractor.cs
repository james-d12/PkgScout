using System.Text.Json;
using PkgScout.Detection.Application.Npm.Models;

namespace PkgScout.Detection.Application.Npm.Extractors;

public sealed class NpmPackageLockJsonExtractor : INpmExtractor
{
    public NpmFileType SupportedType => NpmFileType.PackageLockFile;

    public IEnumerable<ApplicationPackage> Extract(string content, NpmFile file)
    {
        var packageJson = JsonSerializer.Deserialize<NpmPackageLockJson>(content);

        if (packageJson is null)
        {
            return [];
        }

        var dependencies = new List<ApplicationPackage>();

        dependencies.AddRange(packageJson.Dependencies.Values.Select(
            d => new ApplicationPackage(
                Name: d.Resolved,
                Version: d.Version,
                Project: file.ScannedFile.Filename,
                Source: ApplicationPackageSource.Npm
            )));
        dependencies.AddRange(packageJson.Packages.Values.Select(
            d => new ApplicationPackage(
                Name: d.Resolved,
                Version: d.Version,
                Project: file.ScannedFile.Filename,
                Source: ApplicationPackageSource.Npm
            )));

        return dependencies;
    }
}