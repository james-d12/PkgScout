using System.Collections.Immutable;
using PkgScout.Application.NuGet;
using PkgScout.Application.NuGet.Models;
using PkgScout.Application.Shared;

namespace PkgScout.Tests.NuGet;

public sealed class NuGetFileMatcherTests
{
    [Fact]
    public void NuGetFileMatcher_WhenProvidedFilesMatchingSearchPattern_ReturnsMatchedResults()
    {
        // Arrange
        var sut = new NuGetFileMatcher();

        var validFiles = new List<ScannedFile>
        {
            ScannedFile.Create("/home/test/thing/testfile.csproj"),
            ScannedFile.Create("/home/test/thing/testfile.nuspec"),
            ScannedFile.Create("/home/test/thing/Directory.Packages.Props"),
            ScannedFile.Create("/home/test/thing/packages.config")
        };

        var files = new List<ScannedFile>
        {
            ScannedFile.Create("/home/test/thing/testfile.txt"),
            ScannedFile.Create("/home/test/thing/thing.html"),
        };

        files.AddRange(validFiles);

        // Act
        var matches = sut.GetMatches(files.ToImmutableList());

        // Assert
        Assert.Equal(4, matches.Count);
        Assert.IsType<NuGetFile>(matches.FirstOrDefault());
        Assert.Equal(matches.Select(m => m.ScannedFile), validFiles);
    }

    [Fact]
    public void
        NuGetFileMatcher_WhenProvidedFilesMatchingSearchPatternAndAreCapitalised_IgnoresCapitalisationAndReturnsMatchedResults()
    {
        // Arrange
        var sut = new NuGetFileMatcher();

        var validFiles = new List<ScannedFile>
        {
            ScannedFile.Create("/home/test/thing/TESTFILE.CSPROJ"),
            ScannedFile.Create("/home/test/thing/TESTFILE.NUSPEC"),
            ScannedFile.Create("/home/test/thing/DIRECTORY.PACKAGES.PROPS"),
            ScannedFile.Create("/home/test/thing/PACKAGES.CONFIG")
        };

        var files = new List<ScannedFile>
        {
            ScannedFile.Create("/home/test/thing/testfile.txt"),
            ScannedFile.Create("/home/test/thing/thing.html"),
        };

        files.AddRange(validFiles);

        // Act
        var matches = sut.GetMatches(files.ToImmutableList());

        // Assert
        Assert.Equal(4, matches.Count);
        Assert.IsType<NuGetFile>(matches.FirstOrDefault());
        Assert.Equal(matches.Select(m => m.ScannedFile), validFiles);
    }
}