using Microsoft.Extensions.DependencyInjection;
using PkgScout.Apt;
using PkgScout.Cargo;
using PkgScout.Docker;
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
        services.RegisterDocker();
        services.RegisterCargo();
        services.RegisterNpm();
        services.RegisterNuGet();
        services.RegisterApt();
    }

    private static void RegisterApt(this IServiceCollection services)
    {
        services.AddScoped<ISystemDetector, AptDetector>();
    }

    private static void RegisterDocker(this IServiceCollection services)
    {
        services.AddScoped<DockerFileExtractor>();
        services.AddScoped<DockerFileMatcher>();
        services.AddScoped<IDetector, DockerDetector>();
    }

    private static void RegisterCargo(this IServiceCollection services)
    {
        services.AddScoped<CargoFileExtractor>();
        services.AddScoped<CargoFileMatcher>();
        services.AddScoped<IDetector, CargoDetector>();
    }

    private static void RegisterNpm(this IServiceCollection services)
    {
        services.AddScoped<INpmExtractor, NpmPackageJsonExtractor>();
        services.AddScoped<INpmExtractor, NpmPackageLockJsonExtractor>();
        services.AddScoped<NpmFileExtractor>();
        services.AddScoped<NpmFileMatcher>();
        services.AddScoped<IDetector, NpmDetector>();
    }

    private static void RegisterNuGet(this IServiceCollection services)
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