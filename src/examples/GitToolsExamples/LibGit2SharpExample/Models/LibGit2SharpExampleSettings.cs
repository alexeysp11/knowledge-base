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
    /// Name of the origin.
    /// </summary>
    public string? OriginName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? CheckoutBranchName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? CompareBranchName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? PullBranchName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? PushRefSpec { get; set; }

    /// <summary>
    /// Commit message.
    /// </summary>
    public string? CommitMessage { get; set; }

    /// <summary>
    /// File paths to stage.
    /// </summary>
    public IEnumerable<string> FilePathsStage { get; set; }

    /// <summary>
    /// Credentials of the git user.
    /// </summary>
    public GitUser? GitUser { get; set; }
}