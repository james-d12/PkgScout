using PkgScout.Modules.Applications.Cargo;
using PkgScout.Modules.Applications.Shared;

namespace PkgScout.Modules.Tests.Applications.Cargo;

public sealed class CargoFileMatcherTests
{
    [Fact]
    public void CargoFileMatcher_WhenProvidedFilesMatchingSearchPattern_ReturnsMatchedResults()
    {
        // Arrange
        var sut = new CargoFileMatcher();

        var validFiles = new List<string>
        {
            "/home/test/thing/Cargo.toml"
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
        Assert.Single(matches.Files);
        Assert.Equal(MatchedFileType.Cargo, matches.FileType);
        Assert.Equal(matches.Files, validFiles);
    }
}