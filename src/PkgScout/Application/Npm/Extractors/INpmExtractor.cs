using PkgScout.Application.Npm.Models;
using PkgScout.Application.Shared;

namespace PkgScout.Application.Npm.Extractors;

public interface INpmExtractor
{
    NpmFileType SupportedType { get; }
    IEnumerable<ApplicationPackage> Extract(string content, NpmFile file);
}