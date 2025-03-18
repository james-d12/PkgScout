namespace PkgScout.Detection.Application.NuGet.Models;

public readonly record struct NuGetFile(ScannedFile ScannedFile, NuGetFileType FileType);