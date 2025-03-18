using Microsoft.Extensions.Logging;

namespace PkgScout.Detection;

public static partial class DetectionLogTemplates
{
    [LoggerMessage(1000, LogLevel.Information, "Starting {Platform} detection process.")]
    public static partial void DetectionStarted(this ILogger logger, string platform);

    [LoggerMessage(1001, LogLevel.Warning, "No files found matching {Platform} search criteria.")]
    public static partial void DetectionFilesNotFound(this ILogger logger, string platform);

    [LoggerMessage(1002, LogLevel.Information, "Found {MatchedFileCount} file(s) matching {platform} search criteria.")]
    public static partial void DetectionFilesMatched(this ILogger logger, string platform, int matchedFileCount);

    [LoggerMessage(1003, LogLevel.Error, "{Platform} detection failed due to an exception.")]
    public static partial void DetectionFailed(this ILogger logger, string platform, Exception exception);


    [LoggerMessage(1004, LogLevel.Information, "Starting {Platform} extraction of packages from file: {FilePath}")]
    public static partial void DetectionExtractFileStarted(this ILogger logger, string platform, string filePath);

    [LoggerMessage(1005, LogLevel.Warning, "{Platform} extraction Could not find any extractors for file: {FilePath}")]
    public static partial void DetectionExtractFileExtractorNotFound(this ILogger logger, string platform,
        string filePath);

    [LoggerMessage(1006, LogLevel.Error, "{Platform} extraction failed for file: {FilePath} due to an exception.")]
    public static partial void DetectionExtractFileFailed(this ILogger logger, string platform, string filePath,
        Exception exception);
}