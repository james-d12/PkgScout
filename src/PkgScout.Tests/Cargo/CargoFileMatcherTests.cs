using System.Collections.Immutable;
using PkgScout.Detection.Application;
using PkgScout.Detection.Application.Cargo;
using PkgScout.Detection.Application.Cargo.Models;

namespace PkgScout.Tests.Cargo;

public sealed class CargoFileMatcherTests
{
    [Fact]
    public void CargoFileMatcher_WhenProvidedFilesMatchingSearchPattern_ReturnsMatchedResults()
    {
        // Arrange
        var sut = new CargoFileMatcher();

        var validFiles = new List<ScannedFile>
        {
            ScannedFile.Create("/home/test/thing/Cargo.toml")
        };

        var files = new List<ScannedFile>
        {
            ScannedFile.Create("/home/test/thing/testfile.txt"),
            ScannedFile.Create("/home/test/thing/thing.html")
        };

        files.AddRange(validFiles);

        // Act
        var matches = sut.GetMatches(files.ToImmutableList());

        // Assert
        Assert.Single(matches);
        Assert.IsType<CargoFile>(matches.FirstOrDefault());
        Assert.Equal(matches.Select(m => m.ScannedFile), validFiles);
    }
}