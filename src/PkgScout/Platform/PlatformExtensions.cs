using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PkgScout.Platform.GitHub;

namespace PkgScout.Platform;

public static class PlatformExtensions
{
    public static void RegisterPlatformServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GitHubOptions>().Bind(configuration.GetRequiredSection(nameof(GitHubOptions)));
        services.AddSingleton<GitHubConnectionClient>();
        services.AddScoped<GitHubClient>();
    }
}