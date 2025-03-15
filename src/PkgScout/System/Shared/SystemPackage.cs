using System.Text.Json.Serialization;

namespace PkgScout.System.Shared;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SystemPackageSource
{
    Dpkg
}

public readonly record struct SystemPackage(string Name, string Version, SystemPackageSource Source);