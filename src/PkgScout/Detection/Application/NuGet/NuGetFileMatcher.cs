using System.Collections.Immutable;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Detection.Application.NuGet;

public sealed class NuGetFileMatcher : IFileMatcher<NuGetFile>
{
    private static readonly Dictionary<string, NuGetFileType> SearchExtensionPatterns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {
                ".nuspec", NuGetFileType.NuSpecFile
            },

            {
                ".csproj", NuGetFileType.ProjectFile
            }
        };

    private static readonly Dictionary<string, NuGetFileType> SearchFilenamePatterns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "packages.config", NuGetFileType.PackagesConfigFile },
            { "directory.packages.props", NuGetFileType.DirectoryPackagesProps },
        };

    public ImmutableList<NuGetFile> GetMatches(ImmutableList<ScannedFile> files)
    {
        return files
            .Where(file => SearchExtensionPatterns.ContainsKey(file.Extension) ||
                           SearchFilenamePatterns.ContainsKey(file.Filename))
            .Select(file =>
            {
                var fileType = SearchExtensionPatterns.TryGetValue(file.Extension, out var extType)
                    ? extType
                    : SearchFilenamePatterns[file.Filename];
                return new NuGetFile(file, fileType);
            })
            .ToImmutableList();
    }
}