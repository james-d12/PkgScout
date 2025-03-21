using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using PkgScout.Console.Filesystem;
using PkgScout.Detection.Application;
using PkgScout.Detection.System;
using PkgScout.Platform.GitHub;
using PkgScout.Registry.Npm;
using PkgScout.Registry.NuGet;
using Spectre.Console.Cli;

namespace PkgScout.Console.Commands;

public sealed class SearchCommand(
    IEnumerable<IApplicationDetector> detectors,
    SystemSelector systemSelector,
    NuGetRegistryClient nuGetRegistryClient,
    NpmRegistryClient npmRegistryClient,
    GitHubPlatformClient githubPlatformClient,
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

        var newtonsoftPackageInfo = await nuGetRegistryClient.GetPackageInfoAsync("newtonsoft.json");
        var reactPackageInfo = await npmRegistryClient.GetPackageInfoAsync("react");
        var repositories = await githubPlatformClient.GetRepositoriesAsync(CancellationToken.None);

        var files = FileScanner.GetFiles(settings.SearchDirectory);

        logger.LogInformation("Searching across {Count} files", files.Count);

        var applicationPackages = GetApplicationPackages(files);
        var systemPackages = await GetSystemPackages();

        JsonFileWriter.WriteToFile($"{settings.OutputDirectory}/pkgscout-app-packages.json", applicationPackages);
        JsonFileWriter.WriteToFile($"{settings.OutputDirectory}/pkgscout-system-packages.json", systemPackages);

        stopWatch.Stop();
        var milliseconds = stopWatch.Elapsed.TotalMilliseconds;

        logger.LogInformation("PkgScout took: {Milliseconds} ms", milliseconds);

        return 0;
    }

    private ConcurrentBag<ApplicationPackage> GetApplicationPackages(ImmutableList<ScannedFile> files)
    {
        var packages = new ConcurrentBag<ApplicationPackage>();

        Parallel.ForEach(detectors, detector =>
        {
            var detectedPackages = detector.Detect(files).ToList();

            foreach (var package in detectedPackages)
            {
                packages.Add(package);
            }
        });
        return packages;
    }

    private async Task<IEnumerable<SystemPackage>> GetSystemPackages()
    {
        var systemDetector = systemSelector.GetSystemDetector();

        if (systemDetector is null)
        {
            return [];
        }

        return await systemDetector.DetectAsync();
    }
}