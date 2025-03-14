using Microsoft.Extensions.Logging;

namespace PkgScout.Shared.Logging;

public static partial class LogTemplates
{
    [LoggerMessage(1000, LogLevel.Information, "Starting {DetectorName} detection process.")]
    public static partial void DetectionStarted(this ILogger logger, string detectorName);

    [LoggerMessage(1001, LogLevel.Warning, "No files found matching {DetectorName} search criteria.")]
    public static partial void FilesNotFound(this ILogger logger, string detectorName);

    [LoggerMessage(1002, LogLevel.Information,
        "Found {MatchedFileCount} file(s) matching {DetectorName} search criteria.")]
    public static partial void FilesMatched(this ILogger logger, string detectorName, int matchedFileCount);

    [LoggerMessage(1003, LogLevel.Error, "{DetectorName} detection failed due to an exception.")]
    public static partial void DetectionFailed(this ILogger logger, string detectorName, Exception exception);
}