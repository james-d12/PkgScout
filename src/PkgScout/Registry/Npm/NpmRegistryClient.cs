using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace PkgScout.Registry.Npm;

public sealed class NpmRegistryClient(ILogger<NpmRegistryClient> logger, HttpClient httpClient)
    : IRegistryClient<NpmPackage?>
{
    public async Task<NpmPackage?> GetPackageInfoAsync(string name)
    {
        try
        {
            logger.LogInformation("Getting Npm package information for {PackageName}", name);
            var url = new Uri($"https://registry.npmjs.org/{name}");
            var result = await httpClient.GetFromJsonAsync<NpmPackageInfo>(url);

            return new NpmPackage
            {
                Name = name,
                Url = url,
                Versions = result.Versions.Select(v => v.Value).ToList()
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Could not get NuGet package information for {PackageName}", name);
            return null;
        }
    }
}