using Microsoft.Extensions.Options;

namespace PkgScout.Platform.GitHub;

public sealed class GitHubClient(
    GitHubConnectionClient gitHubConnectionClient,
    IOptions<GitHubOptions> options) : IPlatformClient
{
    public async Task<List<Repository>> GetRepositoriesAsync(CancellationToken cancellationToken)
    {
        var githubRepositories =
            await gitHubConnectionClient.Client.Repository.GetAllForCurrent().WaitAsync(cancellationToken) ?? [];

        var repositories = new List<Repository>();

        foreach (var githubRepository in githubRepositories)
        {
            var defaultBranch = githubRepository.DefaultBranch;

            var branchRef =
                await gitHubConnectionClient.Client.Git.Reference.Get(
                    owner: options.Value.Owner,
                    name: githubRepository.Name,
                    reference: $"heads/{defaultBranch}");
            var commitSha = branchRef.Object.Sha;

            var commit = await gitHubConnectionClient.Client.Git.Commit.Get(
                owner: options.Value.Owner,
                name: githubRepository.Name,
                reference: commitSha);
            var treeSha = commit.Tree.Sha;

            var treeResponse = await gitHubConnectionClient.Client.Git.Tree.GetRecursive(
                owner: options.Value.Owner,
                name: githubRepository.Name,
                reference: treeSha);

            repositories.Add(new Repository
            {
                Id = githubRepository.Id.ToString(),
                Name = githubRepository.Name,
                Url = githubRepository.HtmlUrl,
                Files = treeResponse?.Tree.Select(t => new RepositoryFile
                {
                    Path = t.Path,
                    Url = t.Url
                }).ToList() ?? []
            });
        }

        return repositories;
    }
}