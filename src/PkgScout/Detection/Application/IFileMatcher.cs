using System.Collections.Immutable;

namespace PkgScout.Detection.Application;

public interface IFileMatcher<T>
{
    public ImmutableList<T> GetMatches(ImmutableList<ScannedFile> files);
}