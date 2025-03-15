using System.Xml.Linq;
using PkgScout.Application.NuGet.Models;
using PkgScout.Application.Shared;

namespace PkgScout.Application.NuGet.Extractors;

public interface INuGetExtractor
{
    NuGetFileType SupportedType { get; }
    IEnumerable<ApplicationPackage> Extract(XDocument xmlDocument, NuGetFile file);
}