using Microsoft.Extensions.DependencyInjection;
using PkgScout.Registry.Npm;
using PkgScout.Registry.NuGet;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace PkgScout.Registry;

public static class RegistryExtensions
{
    public static IServiceCollection RegisterRegistryServices(this IServiceCollection services)
    {
        services.RegisterHttpClientForServices<NuGetRegistryClient>();
        services.RegisterHttpClientForServices<NpmRegistryClient>();

        services.AddScoped<NuGetRegistryClient>();
        services.AddScoped<NpmRegistryClient>();

        return services;
    }

    private static void RegisterHttpClientForServices<T>(this IServiceCollection services) where T : class
    {
        services.AddHttpClient<T>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(10))
            .AddPolicyHandler(GetRetryPolicy());
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                retryAttempt)));
    }
}