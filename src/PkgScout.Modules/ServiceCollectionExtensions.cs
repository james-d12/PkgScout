using Microsoft.Extensions.DependencyInjection;
using PkgScout.Modules.Applications.Cargo;
using PkgScout.Modules.Applications.Npm;
using PkgScout.Modules.Applications.NuGet;
using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules;

public static class ServiceCollectionExtensions
{
    public static void RegisterModules(this IServiceCollection services)
    {
        services.AddScoped<NpmFileMatcher>();
        services.AddScoped<NugetFileMatcher>();
        services.AddScoped<CargoFileMatcher>();
        services.AddScoped<NpmPackageExtractor>();
        services.AddScoped<NugetPackageExtractor>();
        services.AddScoped<CargoPackageExtractor>();
        services.AddScoped<IService, NpmService>();
        services.AddScoped<IService, NugetService>();
        services.AddScoped<IService, CargoService>();
    }
}