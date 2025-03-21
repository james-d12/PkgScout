namespace PkgScout.Platform.GitHub;

public sealed record GitHubOptions
{
    public required string AgentName { get; init; }
    public required string PersonalAccessToken { get; init; }
}