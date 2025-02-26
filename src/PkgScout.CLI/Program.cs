using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PkgScout.CLI;
using PkgScout.CLI.Commands;
using PkgScout.Core;
using PkgScout.Modules.Cargo;
using PkgScout.Modules.Npm;
using PkgScout.Modules.NuGet;
using Spectre.Console.Cli;

var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddLogging();
builder.Services.AddSingleton<SearchCommand>();
builder.Services.AddScoped<IFileMatcher, NpmFileMatcher>();
builder.Services.AddScoped<IFileMatcher, NugetFileMatcher>();
builder.Services.AddScoped<IFileMatcher, CargoFileMatcher>();
builder.Services.AddScoped<NpmPackageExtractor>();
builder.Services.AddScoped<NugetPackageExtractor>();
builder.Services.AddScoped<CargoPackageExtractor>();

var serviceProvider = builder.Services.BuildServiceProvider();

var app = new CommandApp(new TypeRegistrar(serviceProvider));
app.Configure(config =>
{
    config.AddCommand<SearchCommand>("search")
        .WithDescription("Searches for packages within a directory.")
        .WithExample("search", "/home");
});

app.Run(args);

namespace PkgScout.CLI
{
    public class TypeRegistrar : ITypeRegistrar
    {
        private readonly IServiceProvider _serviceProvider;

        public TypeRegistrar(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public ITypeResolver Build() => new TypeResolver(_serviceProvider);

        public void Register(Type service, Type implementation)
        {
            /* Not needed in this example */
        }

        public void RegisterInstance(Type service, object implementation)
        {
            /* Not needed */
        }

        public void RegisterLazy(Type service, Func<object> factory)
        {
            /* Not needed */
        }
    }

    public class TypeResolver : ITypeResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public TypeResolver(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public object? Resolve(Type? type) => type == null ? null : _serviceProvider.GetService(type);
    }
}