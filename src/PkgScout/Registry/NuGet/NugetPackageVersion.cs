using System.Text.Json.Serialization;

namespace PkgScout.Registry.NuGet;

public readonly record struct CatalogEntry
{
    [JsonPropertyName("@id")]
    public required string Id { get; init; }

    [JsonPropertyName("@type")]
    public required string Type { get; init; }

    [JsonPropertyName("authors")]
    public required string Authors { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("iconUrl")]
    public required string IconUrl { get; init; }

    [JsonPropertyName("id")]
    public required string PackageId { get; init; }

    [JsonPropertyName("language")]
    public required string Language { get; init; }

    [JsonPropertyName("licenseExpression")]
    public required string LicenseExpression { get; init; }

    [JsonPropertyName("licenseUrl")]
    public required string LicenseUrl { get; init; }

    [JsonPropertyName("listed")]
    public required bool Listed { get; init; }

    [JsonPropertyName("minClientVersion")]
    public required string MinClientVersion { get; init; }

    [JsonPropertyName("packageContent")]
    public required string PackageContent { get; init; }

    [JsonPropertyName("projectUrl")]
    public required string ProjectUrl { get; init; }

    [JsonPropertyName("published")]
    public required string Published { get; init; }

    [JsonPropertyName("requireLicenseAcceptance")]
    public required bool RequireLicenseAcceptance { get; init; }

    [JsonPropertyName("summary")]
    public required string Summary { get; init; }

    [JsonPropertyName("tags")]
    public required List<string> Tags { get; init; }

    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("version")]
    public required string Version { get; init; }
}

public readonly record struct NugetPackageContainer
{
    [JsonPropertyName("@id")]
    public required string Id { get; init; }

    [JsonPropertyName("@type")]
    public required List<string> Type { get; init; }

    [JsonPropertyName("commitId")]
    public required string CommitId { get; init; }

    [JsonPropertyName("commitTimeStamp")]
    public required string CommitTimeStamp { get; init; }

    [JsonPropertyName("count")]
    public required int Count { get; init; }

    [JsonPropertyName("items")]
    public required List<NugetPackageContainer2> Packages { get; init; }
}

public readonly record struct NugetPackageContainer2
{
    [JsonPropertyName("@id")]
    public required string Id { get; init; }

    [JsonPropertyName("@type")]
    public required string Type { get; init; }

    [JsonPropertyName("commitId")]
    public required string CommitId { get; init; }

    [JsonPropertyName("commitTimeStamp")]
    public required string CommitTimeStamp { get; init; }

    [JsonPropertyName("count")]
    public required int Count { get; init; }

    [JsonPropertyName("items")]
    public required List<NugetPackageVersion> Versions { get; init; }
}

public readonly record struct NugetPackageVersion
{
    [JsonPropertyName("@id")]
    public required string Id { get; init; }

    [JsonPropertyName("@type")]
    public required string Type { get; init; }

    [JsonPropertyName("commitId")]
    public required string CommitId { get; init; }

    [JsonPropertyName("commitTimeStamp")]
    public required string CommitTimeStamp { get; init; }

    [JsonPropertyName("packageContent")]
    public required string PackageContent { get; init; }

    [JsonPropertyName("registration")]
    public required string Registration { get; init; }

    [JsonPropertyName("catalogEntry")]
    public required CatalogEntry CatalogEntry { get; init; }
}