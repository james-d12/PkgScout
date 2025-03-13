using Spectre.Console.Cli;

namespace PkgScout.Console;

internal sealed class TypeResolver(IServiceProvider serviceProvider) : ITypeResolver
{
    public object? Resolve(Type? type) => type == null ? null : serviceProvider.GetService(type);
}