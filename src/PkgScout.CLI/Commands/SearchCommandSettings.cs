using Spectre.Console.Cli;

namespace PkgScout.CLI.Commands;

public sealed class SearchCommandSettings : CommandSettings
{
    [CommandArgument(0, "<Directory>")]
    public required string Directory { get; set; }
}