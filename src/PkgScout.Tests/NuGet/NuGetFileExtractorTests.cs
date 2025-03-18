using Microsoft.Extensions.Logging.Abstractions;
using PkgScout.Detection.Application;
using PkgScout.Detection.Application.NuGet;
using PkgScout.Detection.Application.NuGet.Extractors;
using PkgScout.Detection.Application.NuGet.Models;

namespace PkgScout.Tests.NuGet;

public sealed class NuGetFileExtractorTests
{
    private readonly NullLogger<NuGetFileExtractor> _nullLogger = new();
    private readonly NuGetFileExtractor _sut;

    public NuGetFileExtractorTests()
    {
        var extractors = new List<INuGetExtractor>
        {
            new NuGetNuspecFileExtractor(),
            new NuGetProjectFileExtractor(),
            new NuGetDirectoryPackagesPropsExtractor(),
            new NuGetPackagesConfigFileExtractor()
        };
        _sut = new NuGetFileExtractor(_nullLogger, extractors);
    }

    [Fact]
    public void NugetPackageExtractor_WhenCsProjFileWithPackageReferencesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath($"{NuGetTestHelper.AssetPath}/ExampleProject.csproj");
        var nugetFile = new NuGetFile(ScannedFile.Create(file), NuGetFileType.ProjectFile);

        var expectedPackages = new List<ApplicationPackage>
        {
            new("Microsoft.Extensions.DependencyInjection.Abstractions", "9.0.2", file,
                ApplicationPackageSource.Nuget),
            new("Microsoft.Extensions.Logging.Console", "9.0.2", file,
                ApplicationPackageSource.Nuget),
            new("Spectre.Console.Cli", "0.49.1", file,
                ApplicationPackageSource.Nuget)
        };

        // Act
        var packages = _sut.Extract(nugetFile).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.True(expectedPackages.SequenceEqual(packages));
    }

    [Fact]
    public void NugetPackageExtractor_WhenPackageConfigFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath($"{NuGetTestHelper.AssetPath}/packages.config");
        var nugetFile = new NuGetFile(ScannedFile.Create(file), NuGetFileType.PackagesConfigFile);

        var expectedPackages = new List<ApplicationPackage>
        {
            new("Newtonsoft.Json", "13.0.1", file, ApplicationPackageSource.Nuget),
            new("Serilog", "2.10.0", file, ApplicationPackageSource.Nuget),
            new("Microsoft.EntityFrameworkCore", "6.0.0", file, ApplicationPackageSource.Nuget)
        };

        // Act
        var packages = _sut.Extract(nugetFile).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.True(expectedPackages.SequenceEqual(packages));
    }

    [Fact]
    public void NugetPackageExtractor_WhenNuSpecFileWithPackagesIsProvided_ShouldReturnNugetPackages()
    {
        // Arrange
        var file = Path.GetFullPath($"{NuGetTestHelper.AssetPath}/Example.nuspec");
        var nugetFile = new NuGetFile(ScannedFile.Create(file), NuGetFileType.NuSpecFile);

        var expectedPackages = new List<ApplicationPackage>
        {
            new("MyLibrary", "1.0.0", file, ApplicationPackageSource.Nuget)
        };

        // Act
        var packages = _sut.Extract(nugetFile).ToList();

        // Assert
        Assert.Equal(expectedPackages.Count, packages.Count);
        Assert.True(expectedPackages.SequenceEqual(packages));
    }
}