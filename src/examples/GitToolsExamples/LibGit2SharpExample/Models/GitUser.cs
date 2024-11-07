namespace WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.Models;

/// <summary>
/// Credentials of the git user.
/// </summary>
public class GitUser
{
    /// <summary>
    /// Username.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    public string? Password { get; set; }
}