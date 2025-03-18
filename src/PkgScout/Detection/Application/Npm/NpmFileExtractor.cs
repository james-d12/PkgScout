using Microsoft.Extensions.Logging;
using PkgScout.Detection.Application.Npm.Extractors;
using PkgScout.Detection.Application.Npm.Models;

namespace PkgScout.Detection.Application.Npm;

public sealed class NpmFileExtractor : IFileExtractor<NpmFile>
{
    private readonly ILogger<NpmFileExtractor> _logger;
    private readonly Dictionary<NpmFileType, INpmExtractor> _extractors;

    public NpmFileExtractor(
        ILogger<NpmFileExtractor> logger,
        IEnumerable<INpmExtractor> extractors)
    {
        _logger = logger;
        _extractors = extractors.ToDictionary(e => e.SupportedType);
    }

    public IEnumerable<ApplicationPackage> Extract(NpmFile file)
    {
        try
        {
            _logger.DetectionExtractFileStarted("Npm", file.ScannedFile.Fullpath);

            if (!_extractors.TryGetValue(file.FileType, out var extractor))
            {
                _logger.DetectionExtractFileExtractorNotFound("Npm", file.ScannedFile.Fullpath);
                return [];
            }

            var content = File.ReadAllText(file.ScannedFile.Fullpath);
            return extractor.Extract(content, file);
        }
        catch (Exception exception)
        {
            _logger.DetectionExtractFileFailed("Npm", file.ScannedFile.Fullpath, exception);
            return [];
        }
    }
}