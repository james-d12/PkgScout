namespace PkgScout.Registry.NuGet;

public readonly record struct NugetPackage
{
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required List<NugetPackageVersion> Versions { get; init; }
}