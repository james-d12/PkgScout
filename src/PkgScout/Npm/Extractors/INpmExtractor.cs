using PkgScout.Shared;

namespace PkgScout.Npm.Extractors;

public interface INpmExtractor
{
    NpmFileType SupportedType { get; }
    IEnumerable<Package> Extract(string content);
}