using System.Xml.Linq;
using PkgScout.NuGet.Extractors;
using PkgScout.Shared;

namespace PkgScout.Tests.NuGet;

public sealed class NuGetNuspecFileExtractorTests
{
    [Fact]
    public void NugetPackageExtractor_WhenNuSpecFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var sut = new NuGetNuspecFileExtractor();
        var file = Path.GetFullPath("./Applications/NuGet/Assets/Example.nuspec");
        var xmlDocument = XDocument.Load(file);

        var expectedPackages = new List<Package>
        {
            new("MyLibrary", "1.0.0")
        };

        // Act
        var packages = sut.Extract(xmlDocument).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.Equal(expectedPackages, packages);
    }
}