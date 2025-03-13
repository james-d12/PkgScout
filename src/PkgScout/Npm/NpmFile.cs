using PkgScout.Shared.Filesystem;

namespace PkgScout.Npm;

public readonly record struct NpmFile(ScannedFile ScannedFile, NpmFileType FileType);