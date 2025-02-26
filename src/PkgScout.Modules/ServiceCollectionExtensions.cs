using Microsoft.Extensions.DependencyInjection;
using PkgScout.Core;
using PkgScout.Modules.Cargo;
using PkgScout.Modules.Npm;
using PkgScout.Modules.NuGet;

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