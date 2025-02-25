using System.Text.Json.Serialization;

namespace PkgScout.Core.Modules.Npm;

[JsonSerializable(typeof(NpmPackageJsonDependency))]
public sealed record NpmPackageJsonDependency(string Name, string Version);

[JsonSerializable(typeof(NpmPackageJson))]
public sealed record NpmPackageJson
{
    [JsonPropertyName("dependencies")]
    public Dictionary<string, string> Dependencies { get; init; } = new();

    [JsonPropertyName("devDependencies")]
    public Dictionary<string, string> DevDependencies { get; init; } = new();
}