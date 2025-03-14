using System.Collections.Immutable;
using PkgScout.Shared.Filesystem;

namespace PkgScout.Shared;

public interface IFileMatcher<T>
{
    public ImmutableList<T> GetMatches(ImmutableList<ScannedFile> files);
}