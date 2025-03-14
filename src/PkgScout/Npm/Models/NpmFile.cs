using PkgScout.Shared.Filesystem;

namespace PkgScout.Npm.Models;

public readonly record struct NpmFile(ScannedFile ScannedFile, NpmFileType FileType);