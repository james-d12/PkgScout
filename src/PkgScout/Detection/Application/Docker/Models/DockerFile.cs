namespace PkgScout.Detection.Application.Docker.Models;

public readonly record struct DockerFile(ScannedFile ScannedFile, DockerFileType FileType);