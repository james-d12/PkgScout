using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using PkgScout.NuGet.Extractors;
using PkgScout.NuGet.Models;
using PkgScout.Shared;

namespace PkgScout.NuGet;

public sealed class NuGetFileExtractor : IFileExtractor<NuGetFile>
{
    private readonly ILogger<NuGetFileExtractor> _logger;
    private readonly Dictionary<NuGetFileType, INuGetExtractor> _extractors;

    public NuGetFileExtractor(
        ILogger<NuGetFileExtractor> logger,
        IEnumerable<INuGetExtractor> extractors)
    {
        _logger = logger;
        _extractors = extractors.ToDictionary(e => e.SupportedType);
    }

    public IEnumerable<Package> Extract(NuGetFile file)
    {
        _logger.LogInformation("Extracting Packages from file: {File} {Type}",
            file.ScannedFile.Fullpath, file.FileType);

        if (!_extractors.TryGetValue(file.FileType, out var extractor))
        {
            _logger.LogWarning("Could not find any extractors for File Type: {FileType}", file.FileType);
            return [];
        }

        var xmlDocument = XDocument.Load(file.ScannedFile.Fullpath);
        return extractor.Extract(xmlDocument, file);
    }
}