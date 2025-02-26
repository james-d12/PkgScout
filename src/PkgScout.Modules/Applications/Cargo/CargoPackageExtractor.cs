using System.Text.RegularExpressions;
using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Applications.Cargo;

public sealed class CargoPackageExtractor : IPackageExtractor
{
    public IEnumerable<Package> Extract(string file)
    {
        var lines = File.ReadAllLines(file).Select(line => line.Trim());

        var dependencies = new List<Package>();
        var isInDependenciesSection = false;
        var dependencyPattern = @"^([\w-]+)\s*=\s*""([^""]+)""";

        foreach (var line in lines)
        {
            if (line.StartsWith('[') && !line.StartsWith("[dependencies]"))
            {
                // Stop parsing when a new section starts
                if (isInDependenciesSection) break;
            }

            if (line.Equals("[dependencies]"))
            {
                isInDependenciesSection = true;
                continue;
            }

            if (isInDependenciesSection)
            {
                var match = Regex.Match(line, dependencyPattern);

                if (match.Success)
                {
                    dependencies.AddRange(new Package(match.Groups[1].Value, match.Groups[2].Value));
                }
            }
        }

        return dependencies;
    }
}