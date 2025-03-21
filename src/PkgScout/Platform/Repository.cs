namespace PkgScout.Platform;

public readonly record struct Repository
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required List<RepositoryFile> Files { get; init; }
}