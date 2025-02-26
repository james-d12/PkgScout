using System.Xml.Linq;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public sealed class NuGetDirectoryPackagesPropsExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.DirectoryPackagesProps;

    public IEnumerable<Package> Extract(XDocument xmlDocument)
    {
        return xmlDocument
            .Descendants()
            .Where(e => e.Name.LocalName.Equals("packageversion", StringComparison.OrdinalIgnoreCase))
            .Select(pr => new Package
            (
                Name: pr.Attribute("Include")?.Value ?? string.Empty,
                Version: pr.Attribute("Version")?.Value ?? "Unknown"
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }
}