using System.Xml.Linq;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Detection.Application.NuGet.Extractors;

public interface INuGetExtractor
{
    NuGetFileType SupportedType { get; }
    IEnumerable<ApplicationPackage> Extract(XDocument xmlDocument, NuGetFile file);
}