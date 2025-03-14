using Microsoft.Extensions.DependencyInjection;
using PkgScout.Cargo;
using PkgScout.Npm;
using PkgScout.Npm.Extractors;
using PkgScout.NuGet;
using PkgScout.NuGet.Extractors;
using PkgScout.Shared;

namespace PkgScout;

public static class ServiceCollectionExtensions
{
    public static void RegisterModules(this IServiceCollection services)
    {
        RegisterCargo(services);
        RegisterNpm(services);
        RegisterNuGet(services);
    }

    private static void RegisterCargo(IServiceCollection services)
    {
        services.AddScoped<CargoPackageExtractor>();
        services.AddScoped<CargoFileMatcher>();
        services.AddScoped<IDetector, CargoDetector>();
    }

    private static void RegisterNpm(IServiceCollection services)
    {
        services.AddScoped<INpmExtractor, NpmPackageJsonExtractor>();
        services.AddScoped<INpmExtractor, NpmPackageLockJsonExtractor>();
        services.AddScoped<NpmFileExtractor>();
        services.AddScoped<NpmFileMatcher>();
        services.AddScoped<IDetector, NpmDetector>();
    }

    private static void RegisterNuGet(IServiceCollection services)
    {
        services.AddScoped<INuGetExtractor, NuGetNuspecFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetPackagesConfigFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetProjectFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetDirectoryPackagesPropsExtractor>();
        services.AddScoped<NuGetFileExtractor>();
        services.AddScoped<NuGetFileMatcher>();
        services.AddScoped<IDetector, NuGetDetector>();
    }
}