using PkgScout.Core;
using PkgScout.Modules.NuGet;

namespace PkgScout.Modules.Tests.NuGet;

public sealed class NugetFileMatcherTests
{
    [Fact]
    public void NugetFileMatcher_WhenProvidedFilesMatchingSearchPattern_ReturnsMatchedResults()
    {
        // Arrange
        var sut = new NugetFileMatcher();

        var validFiles = new List<string>
        {
            "testfile.csproj",
            "testfile.nuspec",
            "test.nupkg",
            "Directory.Packages.Props",
            "packages.config",
        };

        var files = new List<string>
        {
            "testfile.txt",
            "thing.html"
        };

        files.AddRange(validFiles);

        // Act
        var matches = sut.GetMatches(files);

        // Assert
        Assert.Equal(5, matches.Files.Count());
        Assert.Equal(MatchedFileType.NuGet, matches.FileType);
        Assert.Equal(matches.Files, validFiles);
    }

    [Fact]
    public void
        NugetFileMatcher_WhenProvidedFilesMatchingSearchPatternAndAreCapitalised_IgnoresCapitalisationAndReturnsMatchedResults()
    {
        // Arrange
        var sut = new NugetFileMatcher();

        var validFiles = new List<string>
        {
            "TESTFILE.CSPROJ",
            "TESTFILE.NUSPEC",
            "TEST.NUPKG",
            "DIRECTORY.PACKAGES.PROPS",
            "PACKAGES.CONFIG",
        };

        var files = new List<string>
        {
            "testfile.txt",
            "thing.html"
        };

        files.AddRange(validFiles);

        // Act
        var matches = sut.GetMatches(files);

        // Assert
        Assert.Equal(5, matches.Files.Count());
        Assert.Equal(MatchedFileType.NuGet, matches.FileType);
        Assert.Equal(matches.Files, validFiles);
    }
}