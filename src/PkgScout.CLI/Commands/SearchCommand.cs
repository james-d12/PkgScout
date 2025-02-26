using PkgScout.Core;
using Spectre.Console.Cli;

namespace PkgScout.CLI.Commands;

public sealed class SearchCommand(IEnumerable<IService> services) : Command<SearchCommandSettings>
{
    public override int Execute(CommandContext context, SearchCommandSettings settings)
    {
        var files = Directory.EnumerateFiles(settings.Directory, "*.*", SearchOption.AllDirectories).ToList();

        foreach (var service in services)
        {
            service.Start(files);
        }

        return 0;
    }
}