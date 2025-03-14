using System.Text.Json.Serialization;

namespace PkgScout.Shared;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PackageSource
{
    Nuget,
    Npm,
    Cargo,
    Docker,
    Apt
}

public readonly record struct Package(string Name, string Version, string Project, PackageSource PackageSource);