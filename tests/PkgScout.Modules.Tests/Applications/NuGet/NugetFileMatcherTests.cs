using PkgScout.Modules.Applications.NuGet;
using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Tests.Applications.NuGet;

public sealed class NugetFileMatcherTests
{
    [Fact]
    public void NugetFileMatcher_WhenProvidedFilesMatchingSearchPattern_ReturnsMatchedResults()
    {
        // Arrange
        var sut = new NugetFileMatcher();

        var validFiles = new List<string>
        {
            "/home/test/thing/testfile.csproj",
            "/home/test/thing/testfile.nuspec",
            "/home/test/thing/test.nupkg",
            "/home/test/thing/Directory.Packages.Props",
            "/home/test/thing/packages.config",
        };

        var files = new List<string>
        {
            "/home/test/thing/testfile.txt",
            "/home/test/thing/thing.html"
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