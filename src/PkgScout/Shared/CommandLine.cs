using System.Diagnostics;
using System.Text;

namespace PkgScout.Shared;

public static class CommandLine
{
    public static async Task<string> ExecuteAndReturnStdOutAsync(string command, string arguments)
    {
        var stdOut = new StringBuilder();
        var stdErr = new StringBuilder();

        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = startInfo;

        process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null)
                stdOut.AppendLine(e.Data);
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
                stdErr.AppendLine(e.Data);
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync();

        return stdOut.ToString();
    }

    public static async Task<bool> ExecuteAsync(string command, string arguments)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = startInfo;

        process.Start();

        await process.WaitForExitAsync();

        return process.ExitCode == 0;
    }
}