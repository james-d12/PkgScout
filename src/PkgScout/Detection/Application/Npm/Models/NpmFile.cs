namespace PkgScout.Detection.Application.Npm.Models;

public readonly record struct NpmFile(ScannedFile ScannedFile, NpmFileType FileType);