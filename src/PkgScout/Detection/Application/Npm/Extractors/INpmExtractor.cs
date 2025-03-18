using PkgScout.Detection.Application.Npm.Models;

namespace PkgScout.Detection.Application.Npm.Extractors;

public interface INpmExtractor
{
    NpmFileType SupportedType { get; }
    IEnumerable<ApplicationPackage> Extract(string content, NpmFile file);
}