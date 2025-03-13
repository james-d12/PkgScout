using System.Text.RegularExpressions;
using PkgScout.Shared;

namespace PkgScout.Cargo;

public sealed class CargoPackageExtractor
{
    public IEnumerable<Package> Extract(CargoFile file)
    {
        var lines = File.ReadAllLines(file.ScannedFile.Fullpath).Select(line => line.Trim());

        var dependencies = new List<Package>();
        var isInDependenciesSection = false;
        const string dependencyPattern = @"^([\w-]+)\s*=\s*""([^""]+)""";

        foreach (var line in lines)
        {
            if (line.StartsWith('[') && !line.StartsWith("[dependencies]"))
            {
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
                    dependencies.AddRange(new Package(
                        Name: match.Groups[1].Value,
                        Version: match.Groups[2].Value,
                        Project: file.ScannedFile.Fullpath,
                        Source: PackageSource.Cargo
                    ));
                }
            }
        }

        return dependencies;
    }
}