using System.Collections.Immutable;
using PkgScout.Detection.Application.Docker.Models;

namespace PkgScout.Detection.Application.Docker;

public sealed class DockerFileMatcher : IFileMatcher<DockerFile>
{
    private static readonly Dictionary<string, DockerFileType> FileNamePatterns =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "Dockerfile", DockerFileType.DockerFile },
            { "docker-compose.yml", DockerFileType.DockerComposeFile },
            { "docker-compose.yaml", DockerFileType.DockerComposeFile }
        };

    public ImmutableList<DockerFile> GetMatches(ImmutableList<ScannedFile> files)
    {
        return files
            .Where(file => FileNamePatterns.ContainsKey(file.Filename))
            .Select(file =>
            {
                var fileType = FileNamePatterns[file.Filename];
                return new DockerFile(file, fileType);
            })
            .ToImmutableList();
    }
}