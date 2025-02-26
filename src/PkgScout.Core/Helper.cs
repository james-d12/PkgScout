namespace PkgScout.Core;

public static class Helper
{
    public static void PrintPackages(string file, IEnumerable<Package> packages)
    {
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine($"Project File {file} Packages:");
        foreach (var package in packages)
        {
            Console.WriteLine($"    Package: {package.Name} {package.Version}");
        }
    }
}