using PkgScout.Shared;

namespace PkgScout.Download;

public sealed record DownloadedRepository
{
    public required string Name { get; init; }
    public required string Branch { get; init; }
    public required string Path { get; init; }
    public required string Url { get; init; }
}

public sealed class GitDownloader
{
    public async Task<DownloadedRepository?> DownloadRepositoryAsync(string name, string url, string outputDirectory,
        string branch)
    {
        var arguments = $"clone --depth 1 {url} {outputDirectory} --branch {branch}";
        var result = await CommandLine.ExecuteAsync("git", arguments);

        if (!result) return null;

        return new DownloadedRepository
        {
            Name = name,
            Branch = branch,
            Path = outputDirectory,
            Url = url
        };
    }
}