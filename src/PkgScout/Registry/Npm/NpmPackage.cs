using System.Text.Json.Serialization;

namespace PkgScout.Registry.Npm;

public readonly record struct NpmPackageVersionLicense
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

public readonly record struct NpmPackageVersionRepository
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

public readonly record struct NpmPackageVersion
{
    [JsonPropertyName("_id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("version")]
    public required string Version { get; init; }

    [JsonPropertyName("repository")]
    public required NpmPackageVersionRepository Repository { get; init; }

    [JsonPropertyName("licenses")]
    public List<NpmPackageVersionLicense>? Licenses { get; init; }
}

public readonly record struct NpmPackageInfo
{
    [JsonPropertyName("_id")]
    public required string Id { get; init; }

    [JsonPropertyName("_rev")]
    public required string Rev { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("versions")]
    public required Dictionary<string, NpmPackageVersion> Versions { get; init; }
}

public readonly record struct NpmPackage
{
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required List<NpmPackageVersion> Versions { get; init; }
}