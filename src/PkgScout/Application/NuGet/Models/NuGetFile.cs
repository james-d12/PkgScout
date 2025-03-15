using PkgScout.Application.Shared;

namespace PkgScout.Application.NuGet.Models;

public readonly record struct NuGetFile(ScannedFile ScannedFile, NuGetFileType FileType);