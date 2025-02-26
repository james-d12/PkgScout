namespace PkgScout.Modules.Applications.Shared;

public enum MatchedFileType
{
    NuGet = 0,
    Npm,
    Cargo
}

public readonly record struct MatchedFiles(IEnumerable<string> Files, MatchedFileType FileType);

public interface IFileMatcher
{
    MatchedFiles GetMatches(IReadOnlyList<string> files);
}