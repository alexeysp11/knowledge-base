namespace WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.Models;

/// <summary>
/// Application settings.
/// </summary>
public class LibGit2SharpExampleSettings
{
    /// <summary>
    /// Remote repository URL.
    /// </summary>
    public string? RepositoryUrl { get; set; }

    /// <summary>
    /// Local repository path.
    /// </summary>
    public string? RepositoryPath { get; set; }

    /// <summary>
    /// Credentials of the git user.
    /// </summary>
    public GitUser? GitUser { get; set; }
}