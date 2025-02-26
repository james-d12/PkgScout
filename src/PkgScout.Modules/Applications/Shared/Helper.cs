namespace PkgScout.Modules.Applications.Shared;

internal static class Helper
{
    internal static void PrintPackages(string file, IEnumerable<Package> packages)
    {
        if (!packages.Any())
        {
            return;
        }

        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine($"Project File {file} Packages:");
        foreach (var package in packages)
        {
            Console.WriteLine($"    Package: {package.Name} {package.Version}");
        }
    }
}