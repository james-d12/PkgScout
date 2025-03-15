using PkgScout.Application.Shared;

namespace PkgScout.Application.Npm.Models;

public readonly record struct NpmFile(ScannedFile ScannedFile, NpmFileType FileType);