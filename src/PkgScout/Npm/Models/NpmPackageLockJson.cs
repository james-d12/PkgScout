using System.Text.Json.Serialization;

namespace PkgScout.Npm.Models;

[JsonSerializable(typeof(NpmPackageLockJson))]
public record NpmPackageLockJson
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("version")]
    public required string Version { get; init; }

    [JsonPropertyName("lockfileVersion")]
    public required int LockfileVersion { get; init; }

    [JsonPropertyName("requires")]
    public required bool Requires { get; init; }

    [JsonPropertyName("packages")]
    public Dictionary<string, NpmPackageLockPackage> Packages { get; init; } = [];

    [JsonPropertyName("dependencies")]
    public Dictionary<string, NpmPackageLockDependency> Dependencies { get; init; } = [];
}

[JsonSerializable(typeof(NpmPackageLockPackage))]
public sealed record NpmPackageLockPackage
{
    [JsonPropertyName("version")]
    public required string Version { get; init; }

    [JsonPropertyName("resolved")]
    public required string Resolved { get; init; }

    [JsonPropertyName("integrity")]
    public required string Integrity { get; init; }

    [JsonPropertyName("dev")]
    public required bool Dev { get; init; }
}

[JsonSerializable(typeof(NpmPackageLockDependency))]
public sealed record NpmPackageLockDependency
{
    [JsonPropertyName("version")]
    public required string Version { get; init; }

    [JsonPropertyName("resolved")]
    public required string Resolved { get; init; }

    [JsonPropertyName("integrity")]
    public required string Integrity { get; init; }

    [JsonPropertyName("requires")]
    public Dictionary<string, string>? Requires { get; init; }

    [JsonPropertyName("dev")]
    public required bool Dev { get; init; }
}