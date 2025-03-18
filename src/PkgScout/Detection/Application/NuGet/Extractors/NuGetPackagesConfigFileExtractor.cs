using System.Xml.Linq;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Detection.Application.NuGet.Extractors;

public sealed class NuGetPackagesConfigFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.PackagesConfigFile;

    public IEnumerable<ApplicationPackage> Extract(XDocument xmlDocument, NuGetFile file)
    {
        return xmlDocument
            .Descendants()
            .Where(e => e.Name.LocalName.Equals("package", StringComparison.OrdinalIgnoreCase))
            .Select(pr => new ApplicationPackage
            (
                Name: pr.Attribute("id")?.Value ?? string.Empty,
                Version: pr.Attribute("version")?.Value ?? "Unknown",
                Project: file.ScannedFile.Fullpath,
                Source: ApplicationPackageSource.Nuget
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }
}