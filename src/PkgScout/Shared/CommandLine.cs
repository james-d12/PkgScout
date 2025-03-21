using System.Text;
using CliWrap;

namespace PkgScout.Shared;

public static class CommandLine
{
    public static async Task<string> ExecuteAndReturnStdOutAsync(string command, string arguments)
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

    public static async Task<bool> ExecuteAsync(string command, string arguments)
    {
        var result = await Cli.Wrap(command)
            .WithArguments(arguments)
            .ExecuteAsync();

        return result.IsSuccess;
    }
}