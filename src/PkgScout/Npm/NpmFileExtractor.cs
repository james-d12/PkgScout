using Microsoft.Extensions.Logging;
using PkgScout.Npm.Extractors;
using PkgScout.Npm.Models;
using PkgScout.Shared;

namespace PkgScout.Npm;

public sealed class NpmFileExtractor
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

    public IEnumerable<Package> Extract(NpmFile file)
    {
        try
        {
            _logger.LogInformation("Extracting Packages from file: {File} {Type}",
                file.ScannedFile.Fullpath, file.FileType);

            if (!_extractors.TryGetValue(file.FileType, out var extractor))
            {
                _logger.LogWarning("Could not find any extractors for File Type: {FileType}", file.FileType);
                return [];
            }

            var content = File.ReadAllText(file.ScannedFile.Fullpath);
            return extractor.Extract(content, file);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error extracting Npm package.");
            return [];
        }
    }
}