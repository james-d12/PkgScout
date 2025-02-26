using System.Xml.Linq;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public interface INuGetExtractor
{
    NuGetFileType SupportedType { get; }
    IEnumerable<Package> Extract(XDocument xmlDocument);
}