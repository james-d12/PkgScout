using Microsoft.Extensions.Logging;

namespace PkgScout.Platform.GitHub;

public sealed class GitHubPlatformClient(
    ILogger<GitHubPlatformClient> logger,
    GitHubConnectionClient gitHubConnectionClient) : IPlatformClient
{
    public async Task<List<PlatformRepository>> GetRepositoriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Getting GitHub repositories");
            var githubRepositories =
                await gitHubConnectionClient.Client.Repository.GetAllForCurrent().WaitAsync(cancellationToken) ?? [];

            var repositoryTasks = githubRepositories
                .Select(async repository =>
                {
                    var branches = await gitHubConnectionClient.Client.Repository.Branch
                        .GetAll(repository.Id)
                        .WaitAsync(cancellationToken);

                    return new PlatformRepository
                    {
                        Id = repository.Id.ToString(),
                        Name = repository.Name,
                        Url = repository.HtmlUrl,
                        Branches = branches.Select(b => b.Name).ToList()
                    };
                });

            var repositories = await Task.WhenAll(repositoryTasks);
            return repositories.ToList();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred while getting GitHub repositories");
            return [];
        }
    }
}