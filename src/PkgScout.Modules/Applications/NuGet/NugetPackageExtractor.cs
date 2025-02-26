using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Applications.NuGet;

public sealed class NugetPackageExtractor(ILogger<NugetPackageExtractor> logger) : IPackageExtractor
{
    private const string PackageFilePattern = "packages.config";
    private const string ProjectFileExtension = ".csproj";

    public IEnumerable<Package> Extract(string file)
    {
        var nugetFileType = GetNugetFileType(file);

        if (nugetFileType == NugetFileType.Invalid)
        {
            logger.LogInformation("File: {File} was not a valid NuGet File.", file);
            return [];
        }

        var xmlDocument = XDocument.Load(file);

        return nugetFileType switch
        {
            NugetFileType.ProjectFile => GetPackagesFromProjectFile(xmlDocument),
            NugetFileType.CentralizedPackageManagement => GetPackagesFromCentralisedPackageManagement(xmlDocument),
            NugetFileType.PackageConfigFile => GetPackagesFromPackageConfigFile(xmlDocument),
            NugetFileType.NuSpecFile => GetPackagesFromNuSpecFile(xmlDocument),
            _ => []
        };
    }

    private static IEnumerable<Package> GetPackagesFromProjectFile(XDocument xmlDocument)
    {
        return xmlDocument.Descendants("PackageReference")
            .Select(pr => new Package
            (
                Name: pr.Attribute("Include")?.Value ?? string.Empty,
                Version: pr.Attribute("Version")?.Value ?? "Unknown"
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }

    private static IEnumerable<Package> GetPackagesFromPackageConfigFile(XDocument xmlDocument)
    {
        return xmlDocument.Descendants("Packages")
            .Select(pr => new Package
            (
                Name: pr.Attribute("id")?.Value ?? string.Empty,
                Version: pr.Attribute("version")?.Value ?? "Unknown"
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }

    private static IEnumerable<Package> GetPackagesFromCentralisedPackageManagement(XDocument xmlDocument)
    {
        return xmlDocument.Descendants("PackageVersion")
            .Select(pr => new Package
            (
                Name: pr.Attribute("Include")?.Value ?? string.Empty,
                Version: pr.Attribute("Version")?.Value ?? "Unknown"
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }

    private static IEnumerable<Package> GetPackagesFromNuSpecFile(XDocument xmlDocument)
    {
        return xmlDocument.Descendants("package")
            .Select(pr => new Package
            (
                Name: pr.Attribute("id")?.Value ?? string.Empty,
                Version: pr.Attribute("version")?.Value ?? string.Empty
            ));
    }
    
    private static NugetFileType GetNugetFileType(string file)
    {
        var isPackageFile = PackageFilePattern.Contains(file);
        if (isPackageFile) return NugetFileType.PackageConfigFile;

        var isProjectFile = file.EndsWith(ProjectFileExtension, StringComparison.OrdinalIgnoreCase);
        if (isProjectFile) return NugetFileType.ProjectFile;

        return NugetFileType.Invalid;
    }
}