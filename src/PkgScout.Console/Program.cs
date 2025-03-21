using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PkgScout.Console;
using PkgScout.Console.Commands;
using PkgScout.Detection;
using PkgScout.Platform;
using PkgScout.Registry;
using Spectre.Console.Cli;

var builder = WebApplication.CreateBuilder();

builder.Configuration.AddUserSecrets<Program>();

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddLogging();
builder.Services.AddSingleton<SearchCommand>();
builder.Services.RegisterDetectionServices();
builder.Services.RegisterRegistryServices();
builder.Services.RegisterPlatformServices(builder.Configuration);

var serviceProvider = builder.Services.BuildServiceProvider();

var app = new CommandApp(new TypeRegistrar(serviceProvider));
app.Configure(config =>
{
    config.AddCommand<SearchCommand>("search")
        .WithDescription("Searches for packages within a directory.")
        .WithExample("search", "/home");
});

app.Run(args);