using PkgScout.Shared.Filesystem;

namespace PkgScout.NuGet;

public readonly record struct NuGetFile(ScannedFile ScannedFile, NuGetFileType FileType);