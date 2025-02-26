using Microsoft.Extensions.DependencyInjection;
using PkgScout.Cargo;
using PkgScout.Npm;
using PkgScout.NuGet;
using PkgScout.NuGet.Extractors;
using PkgScout.Shared;

namespace PkgScout;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all services
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterModules(this IServiceCollection services)
    {
        RegisterCargo(services);
        RegisterNpm(services);
        RegisterNuGet(services);
    }

    /// <summary>
    /// Registers all Cargo services.
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterCargo(IServiceCollection services)
    {
        services.AddScoped<CargoPackageExtractor>();
        services.AddScoped<CargoFileMatcher>();
        services.AddScoped<IDetector, CargoDetector>();
    }

    /// <summary>
    /// Registers all required NPM services.
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterNpm(IServiceCollection services)
    {
        services.AddScoped<NpmPackageExtractor>();
        services.AddScoped<NpmFileMatcher>();
        services.AddScoped<IDetector, NpmDetector>();
    }

    /// <summary>
    /// Registers all required NuGet services.
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterNuGet(IServiceCollection services)
    {
        services.AddScoped<INuGetExtractor, NuGetNuspecFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetPackagesConfigFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetProjectFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetDirectoryPackagesPropsExtractor>();
        services.AddScoped<NuGetPackageExtractor>();
        services.AddScoped<NuGetFileMatcher>();
        services.AddScoped<IDetector, NuGetDetector>();
    }
}