using Microsoft.Extensions.DependencyInjection;
using PkgScout.Detection.Application;
using PkgScout.Detection.System;

namespace PkgScout.Detection;

public static class DetectionExtensions
{
    public static void RegisterModules(this IServiceCollection services)
    {
        services.RegisterApplicationServices();
        services.RegisterSystemServices();
    }
}