using PkgScout.Shared.Filesystem;

namespace PkgScout.Docker;

public readonly record struct DockerFile(ScannedFile ScannedFile, DockerFileType FileType);