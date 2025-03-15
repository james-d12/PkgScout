using System.Collections.Immutable;

namespace PkgScout.Application.Shared;

public interface IFileMatcher<T>
{
    public ImmutableList<T> GetMatches(ImmutableList<ScannedFile> files);
}