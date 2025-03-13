using PkgScout.Shared;
using PkgScout.Shared.Filesystem;
using Spectre.Console.Cli;

namespace PkgScout.Console.Commands;

public sealed class SearchCommand(IEnumerable<IDetector> detectors)
    : Command<SearchCommandSettings>
{
    public override int Execute(CommandContext context, SearchCommandSettings settings)
    {
        var files = FileScanner.GetFiles(settings.Directory);

        var packages = detectors.SelectMany(detector => detector.Start(files));

        JsonFileWriter.WriteToFile("/home/james/Downloads/Packages.json", packages);

        return 0;
    }
}