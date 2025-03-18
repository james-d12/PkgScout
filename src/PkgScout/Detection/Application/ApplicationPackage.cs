using System.Text.Json.Serialization;

namespace PkgScout.Detection.Application;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApplicationPackageSource
{
    Nuget,
    Npm,
    Cargo,
    Docker
}

public readonly record struct ApplicationPackage(
    string Name,
    string Version,
    string Project,
    ApplicationPackageSource Source);