using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;
using Spectre.Console.Cli;

namespace PkgScout.Console.Commands;

public sealed class SearchCommand(
    IEnumerable<IDetector> detectors,
    ISystemDetector systemDetector,
    ILogger<SearchCommand> logger)
    : AsyncCommand<SearchCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, SearchCommandSettings settings)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        if (!Directory.Exists(settings.SearchDirectory))
        {
            logger.LogError("Search directory: {Directory} does not exist.", settings.SearchDirectory);
            return 1;
        }

        if (!Directory.Exists(settings.OutputDirectory))
        {
            logger.LogError("Output directory: {Directory} does not exist.", settings.OutputDirectory);
            return 1;
        }

        var files = FileScanner.GetFiles(settings.SearchDirectory);

        logger.LogInformation("Searching across {Count} files", files.Count);

        var packages = new ConcurrentBag<Package>();

        Parallel.ForEach(detectors, detector =>
        {
            var detectedPackages = detector.Detect(files).ToList();

            foreach (var package in detectedPackages)
            {
                packages.Add(package);
            }
        });

        var systemPackages = await systemDetector.DetectAsync();

        foreach (var package in systemPackages)
        {
            packages.Add(package);
        }

        JsonFileWriter.WriteToFile($"{settings.OutputDirectory}/pkgscout-packages.json", packages);

        stopWatch.Stop();
        var milliseconds = stopWatch.Elapsed.TotalMilliseconds;

        logger.LogInformation("PkgScout took: {Milliseconds} ms", milliseconds);


        return 0;
    }
}