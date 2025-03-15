using PkgScout.Application.Shared;

namespace PkgScout.Application.Docker.Models;

public readonly record struct DockerFile(ScannedFile ScannedFile, DockerFileType FileType);