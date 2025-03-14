using System.Xml.Linq;
using PkgScout.NuGet.Models;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public sealed class NuGetNuspecFileExtractor : INuGetExtractor
{
    public NuGetFileType SupportedType => NuGetFileType.NuSpecFile;

    public IEnumerable<Package> Extract(XDocument xmlDocument, NuGetFile file)
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
                select new Package(
                    Name: id,
                    Version: version,
                    Project: file.ScannedFile.Fullpath,
                    PackageSource: PackageSource.Nuget
                )).ToList();
    }
}