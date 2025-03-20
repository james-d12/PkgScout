using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace PkgScout.Registry.NuGet;

public sealed class NuGetRegistryClient(ILogger<NuGetRegistryClient> logger, HttpClient httpClient)
    : IRegistryClient<NugetPackage?>
{
    public async Task<NugetPackage?> GetPackageInfoAsync(string name)
    {
        // https://api.nuget.org/v3/registration5-semver1/newtonsoft.json/index.json
        // https://api.nuget.org/v3/registration5-semver1/newtonsoft.json/13.0.1.json

        try
        {
            logger.LogInformation("Getting NuGet package information for {PackageName}", name);
            var url = new Uri($"https://api.nuget.org/v3/registration5-semver1/{name}/index.json");
            var result = await httpClient.GetFromJsonAsync<NugetPackageContainer>(url);

            return new NugetPackage
            {
                Name = name,
                Url = url,
                Versions = result.Packages.SelectMany(p => p.Versions).ToList()
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Could not get NuGet package information for {PackageName}", name);
            return null;
        }
    }
}