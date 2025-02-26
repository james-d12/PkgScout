using Microsoft.Extensions.Logging.Abstractions;
using PkgScout.Modules.Applications.NuGet;
using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Tests.Applications.NuGet;

public sealed class NugetPackageExtractorTests
{
    private readonly NullLogger<NugetPackageExtractor> _nullLogger = new();
    private readonly NugetPackageExtractor _sut;

    public NugetPackageExtractorTests()
    {
        _sut = new NugetPackageExtractor(_nullLogger);
    }

    [Fact]
    public void NugetPackageExtractor_WhenCsProjFileWithPackageReferencesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath("./Applications/NuGet/Assets/ExampleProject.csproj");

        var expectedPackages = new List<Package>
        {
            new("Microsoft.Extensions.DependencyInjection.Abstractions", "9.0.2"),
            new("Microsoft.Extensions.Logging.Console", "9.0.2"),
            new("Spectre.Console.Cli", "0.49.1")
        };

        // Act
        var packages = _sut.Extract(file).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.Equal(packages, expectedPackages);
    }

    [Fact]
    public void NugetPackageExtractor_WhenPackageConfigFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath("./Applications/NuGet/Assets/packages.config");

        var expectedPackages = new List<Package>
        {
            new("Newtonsoft.Json", "13.0.1"),
            new("Serilog", "2.10.0"),
            new("Microsoft.EntityFrameworkCore", "6.0.0")
        };

        // Act
        var packages = _sut.Extract(file).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.Equal(packages, expectedPackages);
    }

    [Fact]
    public void NugetPackageExtractor_WhenNuSpecFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath("./Applications/NuGet/Assets/Example.nuspec");

        var expectedPackages = new List<Package>
        {
            new("MyLibrary", "1.0.0")
        };

        // Act
        var packages = _sut.Extract(file).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.Equal(expectedPackages, packages);
    }
}