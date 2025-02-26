using Microsoft.Extensions.Logging.Abstractions;
using PkgScout.NuGet;
using PkgScout.NuGet.Extractors;
using PkgScout.Shared;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Tests.NuGet;

public sealed class NuGetPackageExtractorTests
{
    private readonly NullLogger<NuGetPackageExtractor> _nullLogger = new();
    private readonly NuGetPackageExtractor _sut;

    public NuGetPackageExtractorTests()
    {
        var extractors = new List<INuGetExtractor>
        {
            new NuGetNuspecFileExtractor(),
            new NuGetProjectFileExtractor(),
            new NuGetDirectoryPackagesPropsExtractor(),
            new NuGetPackagesConfigFileExtractor()
        };
        _sut = new NuGetPackageExtractor(_nullLogger, extractors);
    }

    [Fact]
    public void NugetPackageExtractor_WhenCsProjFileWithPackageReferencesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath("./Applications/NuGet/Assets/ExampleProject.csproj");
        var nugetFile = new NuGetFile(ScannedFile.Create(file), NuGetFileType.ProjectFile);

        var expectedPackages = new List<Package>
        {
            new("Microsoft.Extensions.DependencyInjection.Abstractions", "9.0.2"),
            new("Microsoft.Extensions.Logging.Console", "9.0.2"),
            new("Spectre.Console.Cli", "0.49.1")
        };

        // Act
        var packages = _sut.Extract(nugetFile).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.Equal(packages, expectedPackages);
    }

    [Fact]
    public void NugetPackageExtractor_WhenPackageConfigFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath("./Applications/NuGet/Assets/packages.config");
        var nugetFile = new NuGetFile(ScannedFile.Create(file), NuGetFileType.PackagesConfigFile);

        var expectedPackages = new List<Package>
        {
            new("Newtonsoft.Json", "13.0.1"),
            new("Serilog", "2.10.0"),
            new("Microsoft.EntityFrameworkCore", "6.0.0")
        };

        // Act
        var packages = _sut.Extract(nugetFile).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.Equal(packages, expectedPackages);
    }

    [Fact]
    public void NugetPackageExtractor_WhenNuSpecFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath("./Applications/NuGet/Assets/Example.nuspec");
        var nugetFile = new NuGetFile(ScannedFile.Create(file), NuGetFileType.NuSpecFile);

        var expectedPackages = new List<Package>
        {
            new("MyLibrary", "1.0.0")
        };

        // Act
        var packages = _sut.Extract(nugetFile).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.Equal(expectedPackages, packages);
    }
}