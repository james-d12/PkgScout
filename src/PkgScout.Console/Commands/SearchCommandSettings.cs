using System.ComponentModel;
using Spectre.Console.Cli;

namespace PkgScout.Console.Commands;

public sealed class SearchCommandSettings : CommandSettings
{
    [CommandOption("--search-directory <SearchDirectory>")]
    [Description("The search directory to search within.")]
    public required string SearchDirectory { get; set; }

    [CommandOption("--output-directory <OutputDirectory>")]
    [Description("The output directory to export the list of packages to.")]
    public required string OutputDirectory { get; set; }
}