using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PkgScout.Detection.System.Dpkg;
using PkgScout.Detection.System.Pacman;
using PkgScout.Detection.System.Winget;

namespace PkgScout.Detection.System;

public static class SystemExtensions
{
    public static void RegisterSystemServices(this IServiceCollection services)
    {
        services.TryAddSingleton<SystemSelector>();
        services.AddScoped<ISystemDetector, DpkgDetector>();
        services.AddScoped<ISystemDetector, PacmanDetector>();
        services.AddScoped<ISystemDetector, WinGetDetector>();
    }
}