using Microsoft.Extensions.DependencyInjection;
using PkgScout.Detection.Application;
using PkgScout.Detection.Application.Cargo;
using PkgScout.Detection.Application.Docker;
using PkgScout.Detection.Application.Npm;
using PkgScout.Detection.Application.Npm.Extractors;
using PkgScout.Detection.Application.NuGet;
using PkgScout.Detection.Application.NuGet.Extractors;
using PkgScout.Detection.System;
using PkgScout.Detection.System.Dpkg;

namespace PkgScout.Detection;

public static class DetectionExtensions
{
    public static void RegisterModules(this IServiceCollection services)
    {
        services.RegisterDocker();
        services.RegisterCargo();
        services.RegisterNpm();
        services.RegisterNuGet();
        services.RegisterDpkg();
    }

    private static void RegisterDpkg(this IServiceCollection services)
    {
        services.AddScoped<ISystemDetector, DpkgDetector>();
    }

    private static void RegisterDocker(this IServiceCollection services)
    {
        services.AddScoped<DockerFileExtractor>();
        services.AddScoped<DockerFileMatcher>();
        services.AddScoped<IApplicationDetector, DockerApplicationDetector>();
    }

    private static void RegisterCargo(this IServiceCollection services)
    {
        services.AddScoped<CargoFileExtractor>();
        services.AddScoped<CargoFileMatcher>();
        services.AddScoped<IApplicationDetector, CargoApplicationDetector>();
    }

    private static void RegisterNpm(this IServiceCollection services)
    {
        services.AddScoped<INpmExtractor, NpmPackageJsonExtractor>();
        services.AddScoped<INpmExtractor, NpmPackageLockJsonExtractor>();
        services.AddScoped<NpmFileExtractor>();
        services.AddScoped<NpmFileMatcher>();
        services.AddScoped<IApplicationDetector, NpmApplicationDetector>();
    }

    private static void RegisterNuGet(this IServiceCollection services)
    {
        services.AddScoped<INuGetExtractor, NuGetNuspecFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetPackagesConfigFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetProjectFileExtractor>();
        services.AddScoped<INuGetExtractor, NuGetDirectoryPackagesPropsExtractor>();
        services.AddScoped<NuGetFileExtractor>();
        services.AddScoped<NuGetFileMatcher>();
        services.AddScoped<IApplicationDetector, NuGetApplicationDetector>();
    }
}