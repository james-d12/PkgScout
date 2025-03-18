using System.Xml.Linq;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Detection.Application.NuGet.Extractors;

public sealed class NuGetDirectoryPackagesPropsExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.DirectoryPackagesProps;

    public IEnumerable<ApplicationPackage> Extract(XDocument xmlDocument, NuGetFile file)
    {
        return xmlDocument
            .Descendants()
            .Where(e => e.Name.LocalName.Equals("packageversion", StringComparison.OrdinalIgnoreCase))
            .Select(pr => new ApplicationPackage
            (
                Name: pr.Attribute("Include")?.Value ?? string.Empty,
                Version: pr.Attribute("Version")?.Value ?? "Unknown",
                Project: file.ScannedFile.Fullpath,
                Source: ApplicationPackageSource.Nuget
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }
}