using System.Text;
using CliWrap;
using Microsoft.Extensions.Logging;

namespace PkgScout.Shared.CommandLine;

public static class CommandLine
{
    public static async Task<string> Execute(string command, string arguments, ILogger logger)
    {
        var stdOut = new StringBuilder();
        var stdErr = new StringBuilder();

        await Cli.Wrap(command)
            .WithArguments(arguments)
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOut))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErr))
            .ExecuteAsync();

        return stdOut.ToString();
    }
}