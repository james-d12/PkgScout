namespace PkgScout.Platform;

public interface IPlatformClient
{
    Task<List<PlatformRepository>> GetRepositoriesAsync(CancellationToken cancellationToken);
}