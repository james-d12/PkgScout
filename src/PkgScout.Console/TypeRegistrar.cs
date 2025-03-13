using Spectre.Console.Cli;

namespace PkgScout.Console;

internal sealed class TypeRegistrar(IServiceProvider serviceProvider) : ITypeRegistrar
{
    public ITypeResolver Build() => new TypeResolver(serviceProvider);

    public void Register(Type service, Type implementation)
    {
    }

    public void RegisterInstance(Type service, object implementation)
    {
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
    }
}