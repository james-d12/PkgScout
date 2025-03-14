using System.Xml.Linq;
using PkgScout.NuGet.Models;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public sealed class NuGetPackagesConfigFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.PackagesConfigFile;

    public IEnumerable<Package> Extract(XDocument xmlDocument, NuGetFile file)
    {
        return xmlDocument
            .Descendants()
            .Where(e => e.Name.LocalName.Equals("package", StringComparison.OrdinalIgnoreCase))
            .Select(pr => new Package
            (
                Name: pr.Attribute("id")?.Value ?? string.Empty,
                Version: pr.Attribute("version")?.Value ?? "Unknown",
                Project: file.ScannedFile.Fullpath,
                Source: PackageSource.Nuget
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }
}