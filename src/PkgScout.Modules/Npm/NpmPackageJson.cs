using System.Text.Json.Serialization;

namespace PkgScout.Modules.Npm;

[JsonSerializable(typeof(NpmPackageJson))]
public sealed record NpmPackageJson
{
    [JsonPropertyName("dependencies")]
    public Dictionary<string, string> Dependencies { get; init; } = [];

    [JsonPropertyName("devDependencies")]
    public Dictionary<string, string> DevDependencies { get; init; } = [];
}