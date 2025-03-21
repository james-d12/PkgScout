namespace PkgScout.Platform;

public readonly record struct RepositoryFile
{
    public required string Path { get; init; }
    public required string Url { get; init; }
}