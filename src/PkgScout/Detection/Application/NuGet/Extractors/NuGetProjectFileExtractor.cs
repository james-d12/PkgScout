using System.Xml.Linq;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Detection.Application.NuGet.Extractors;

public sealed class NuGetProjectFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.ProjectFile;

    public IEnumerable<ApplicationPackage> Extract(XDocument xmlDocument, NuGetFile file)
    {
        return xmlDocument.Descendants("PackageReference")
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