using System.Text;
using CliWrap;

namespace PkgScout.Detection.System;

public static class CommandLine
{
    public static async Task<string> Execute(string command, string arguments)
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