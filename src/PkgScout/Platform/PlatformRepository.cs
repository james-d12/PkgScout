namespace PkgScout.Platform;

public readonly record struct PlatformRepository
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required List<string> Branches { get; init; }
}