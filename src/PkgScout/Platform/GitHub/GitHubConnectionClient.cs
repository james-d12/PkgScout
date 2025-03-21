using Microsoft.Extensions.Options;
using Octokit;

namespace PkgScout.Platform.GitHub;

public sealed class GitHubConnectionClient(IOptions<GitHubOptions> options)
{
    public Octokit.GitHubClient Client { get; } = new(new ProductHeaderValue(options.Value.AgentName))
    {
        Credentials = new Credentials(options.Value.PersonalAccessToken)
    };
}