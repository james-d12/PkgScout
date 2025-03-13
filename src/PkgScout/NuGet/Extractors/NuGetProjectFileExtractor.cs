using System.Xml.Linq;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public sealed class NuGetProjectFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.ProjectFile;

    public IEnumerable<Package> Extract(XDocument xmlDocument, NuGetFile file)
    {
        return xmlDocument.Descendants("PackageReference")
            .Select(pr => new Package
            (
                Name: pr.Attribute("Include")?.Value ?? string.Empty,
                Version: pr.Attribute("Version")?.Value ?? "Unknown",
                Project: file.ScannedFile.Fullpath,
                Source: PackageSource.Nuget
            ))
            .Where(p => !string.IsNullOrEmpty(p.Name));
    }
}