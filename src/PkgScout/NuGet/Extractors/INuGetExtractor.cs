using System.Xml.Linq;
using PkgScout.NuGet.Models;
using PkgScout.Shared;

namespace PkgScout.NuGet.Extractors;

public interface INuGetExtractor
{
    NuGetFileType SupportedType { get; }
    IEnumerable<Package> Extract(XDocument xmlDocument, NuGetFile file);
}