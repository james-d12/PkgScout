using System.Xml.Linq;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public sealed class NuGetPackagesConfigFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.PackagesConfigFile;

    public IEnumerable<Package> Extract(XDocument xmlDocument)
    {
        return xmlDocument
            .Descendants()
            .Where(e => e.Name.LocalName.Equals("package", StringComparison.OrdinalIgnoreCase))
            .Select(pr => new Package
            (
                Name: pr.Attribute("id")?.Value ?? string.Empty,
                Version: pr.Attribute("version")?.Value ?? "Unknown"
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }
}