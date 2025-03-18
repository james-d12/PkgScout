using System.Xml.Linq;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Detection.Application.NuGet.Extractors;

public sealed class NuGetNuspecFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.NuSpecFile;

    public IEnumerable<ApplicationPackage> Extract(XDocument xmlDocument, NuGetFile file)
    {
        var metadata = xmlDocument.Descendants()
            .Where(e => e.Name.LocalName.Equals("metadata", StringComparison.OrdinalIgnoreCase));

        return (from metadataElement in metadata
                let idElement = metadataElement.Descendants()
                    .FirstOrDefault(e => e.Name.LocalName.Equals("id", StringComparison.OrdinalIgnoreCase))
                let versionElement = metadataElement.Descendants()
                    .FirstOrDefault(e => e.Name.LocalName.Equals("version", StringComparison.OrdinalIgnoreCase))
                let id = idElement?.Value ?? string.Empty
                let version = versionElement?.Value ?? string.Empty
                select new ApplicationPackage(
                    Name: id,
                    Version: version,
                    Project: file.ScannedFile.Fullpath,
                    Source: ApplicationPackageSource.Nuget
                )).ToList();
    }
}