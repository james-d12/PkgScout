using PkgScout.Shared.Filesystem;

namespace PkgScout.NuGet.Models;

public readonly record struct NuGetFile(ScannedFile ScannedFile, NuGetFileType FileType);