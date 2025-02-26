using PkgScout.Core;
using PkgScout.Modules.Cargo;
using PkgScout.Modules.Npm;
using PkgScout.Modules.NuGet;
using Spectre.Console.Cli;

namespace PkgScout.CLI.Commands;

public sealed class SearchCommand : Command<SearchCommandSettings>
{
    private readonly IEnumerable<IFileMatcher> _fileMatchers;

    private readonly NpmPackageExtractor _npmPackageExtractor;
    private readonly NugetPackageExtractor _nugetPackageExtractor;
    private readonly CargoPackageExtractor _cargoPackageExtractor;

    public SearchCommand(
        IEnumerable<IFileMatcher> fileMatchers,
        NpmPackageExtractor npmPackageExtractor,
        NugetPackageExtractor nugetPackageExtractor,
        CargoPackageExtractor cargoPackageExtractor)
    {
        _fileMatchers = fileMatchers;
        _npmPackageExtractor = npmPackageExtractor;
        _nugetPackageExtractor = nugetPackageExtractor;
        _cargoPackageExtractor = cargoPackageExtractor;
    }

    public override int Execute(CommandContext context, SearchCommandSettings settings)
    {
        var files = Directory.EnumerateFiles(settings.Directory, "*.*", SearchOption.AllDirectories).ToList();

        foreach (var fileMatcher in _fileMatchers)
        {
            var matchedFiles = fileMatcher.GetMatches(files);

            switch (matchedFiles.FileType)
            {
                case MatchedFileType.NuGet:
                    foreach (var file in matchedFiles.Files)
                    {
                        var nugetPackages = _nugetPackageExtractor.Extract(file);
                        PrintPackages(file, nugetPackages);
                    }

                    break;
                case MatchedFileType.Npm:
                    foreach (var file in matchedFiles.Files)
                    {
                        var npmPackages = _npmPackageExtractor.Extract(file);
                        PrintPackages(file, npmPackages);
                    }

                    break;
                case MatchedFileType.Cargo:
                    foreach (var file in matchedFiles.Files)
                    {
                        var npmPackages = _cargoPackageExtractor.Extract(file);
                        PrintPackages(file, npmPackages);
                    }

                    break;
            }
        }

        return 0;
    }

    private static void PrintPackages(string file, IEnumerable<Package> packages)
    {
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine($"Project File {file} Packages:");
        foreach (var package in packages)
        {
            Console.WriteLine($"    Package: {package.Name} {package.Version}");
        }
    }
}