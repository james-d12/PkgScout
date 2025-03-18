using System.Xml.Linq;
using PkgScout.Detection.Application;
using PkgScout.Detection.Application.NuGet.Extractors;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Tests.NuGet;

public sealed class NuGetNuspecFileExtractorTests
{
    [Fact]
    public void NugetPackageExtractor_WhenNuSpecFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var sut = new NuGetNuspecFileExtractor();
        var filePath = Path.GetFullPath($"{NuGetTestHelper.AssetPath}/Example.nuspec");
        var scannedFile = ScannedFile.Create(Path.GetFullPath($"{NuGetTestHelper.AssetPath}/Example.nuspec"));
        var nugetFile = new NuGetFile(scannedFile, NuGetFileType.NuSpecFile);

        var xmlDocument = XDocument.Load(scannedFile.Fullpath);

        var expectedPackages = new List<ApplicationPackage>
        {
            new("MyLibrary", "1.0.0", filePath, ApplicationPackageSource.Nuget),
        };

        // Act
        var packages = sut.Extract(xmlDocument, nugetFile).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.True(expectedPackages.SequenceEqual(packages));
    }
}