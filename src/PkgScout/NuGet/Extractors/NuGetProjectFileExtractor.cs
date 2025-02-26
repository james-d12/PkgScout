using System.Xml.Linq;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public sealed class NuGetProjectFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.ProjectFile;

    public IEnumerable<Package> Extract(XDocument xmlDocument)
    {
        return xmlDocument.Descendants("PackageReference")
            .Select(pr => new Package
            (
                Name: pr.Attribute("Include")?.Value ?? string.Empty,
                Version: pr.Attribute("Version")?.Value ?? "Unknown"
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }
}