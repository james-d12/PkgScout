using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using PkgScout.Detection.Application.NuGet.Extractors;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Detection.Application.NuGet;

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

    public IEnumerable<ApplicationPackage> Extract(NuGetFile file)
    {
        try
        {
            _logger.DetectionExtractFileStarted("NuGet", file.ScannedFile.Fullpath);

            if (!_extractors.TryGetValue(file.FileType, out var extractor))
            {
                _logger.DetectionExtractFileExtractorNotFound("NuGet", file.ScannedFile.Fullpath);
                return [];
            }

            var xmlDocument = XDocument.Load(file.ScannedFile.Fullpath);
            return extractor.Extract(xmlDocument, file);
        }
        catch (Exception exception)
        {
            _logger.DetectionExtractFileFailed("NuGet", file.ScannedFile.Fullpath, exception);
            return [];
        }
    }
}