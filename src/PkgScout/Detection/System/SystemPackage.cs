using System.Text.Json.Serialization;

namespace PkgScout.Detection.System;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SystemPackageSource
{
    Dpkg
}

public readonly record struct SystemPackage(string Name, string Version, SystemPackageSource Source);