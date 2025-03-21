namespace PkgScout.Platform;

public interface IPlatformClient
{
    Task<List<Repository>> GetRepositoriesAsync(CancellationToken cancellationToken);
}