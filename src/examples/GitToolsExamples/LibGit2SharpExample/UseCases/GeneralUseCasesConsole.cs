using LibGit2Sharp;

namespace WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.UseCases;

/// <summary>
/// General use cases for testing libgit2sharp via console.
/// </summary>
internal class GeneralUseCasesConsole
{
    /// <summary>
    /// Get the list of remote repositories.
    /// </summary>
    /// <param name="repositoryPath">Path to the local repository</param>
    internal static void GetRemotes(string repositoryPath)
    {
        using (var repo = new Repository(repositoryPath))
        {
            var remotes = GeneralGitOperations.GetRemotes(repo);
            foreach (var r in remotes)
            {
                System.Console.WriteLine($"remote: {r.Name} \t url: {r.Url}");
            }
        }
    }

    /// <summary>
    /// Get the list of branches.
    /// </summary>
    /// <param name="repositoryPath">Path to the local repository</param>
    internal static void GetBranches(string repositoryPath)
    {
        using (var repo = new Repository(repositoryPath))
        {
            var branches = GeneralGitOperations.GetBranches(repo);
            foreach (var b in branches)
            {
                System.Console.WriteLine($"branch: {b.CanonicalName} \t FriendlyName: {b.FriendlyName} \t remote: {b.IsRemote} \t head: {b.IsCurrentRepositoryHead}");
            }
        }
    }

    /// <summary>
    /// Fetch changes from the remote repository.
    /// </summary>
    /// <param name="repositoryPath">Path to the local repository</param>
    /// <param name="remoteName">Name of the remote repository</param>
    internal static void FetchChanges(string? repositoryPath, string? remoteName)
    {
        using (var repo = new Repository(repositoryPath))
        {
            GeneralGitOperations.FetchChanges(repo, remoteName);
        }
    }

    /// <summary>
    /// Compare the specified branch with head.
    /// </summary>
    /// <param name="repositoryPath">Path to the local repository</param>
    /// <param name="branchName">Name of the branch that is going to be compared with head</param>
    internal static void CompareBranchWithHead(string? repositoryPath, string branchName)
    {
        using (var repo = new Repository(repositoryPath))
        {
            var patch = GeneralGitOperations.CompareBranchWithHead(repo, branchName);
            foreach (var ptc in patch)
            {
                Console.WriteLine(ptc.Status + " : " + ptc.Path);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repositoryPath"></param>
    /// <param name="filePaths"></param>
    internal static void StageChanges(string? repositoryPath, IEnumerable<string> filePaths)
    {
        using (var repo = new Repository(repositoryPath))
        {
            GeneralGitOperations.StageChanges(repo, filePaths);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repositoryPath"></param>
    /// <param name="commitMessage"></param>
    /// <param name="author"></param>
    /// <param name="committer"></param>
    internal static void CommitChanges(string? repositoryPath, string? commitMessage, Signature author, Signature committer)
    {
        using (var repo = new Repository(repositoryPath))
        {
            GeneralGitOperations.CommitChanges(
                repo,
                commitMessage ?? "",
                author,
                committer);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repositoryPath"></param>
    /// <param name="checkoutBranchName"></param>
    internal static void Checkout(string? repositoryPath, string? checkoutBranchName)
    {
        using (var repo = new Repository(repositoryPath))
        {
            GeneralGitOperations.Checkout(repo, checkoutBranchName ?? "");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repositoryPath"></param>
    /// <param name="originName"></param>
    /// <param name="pullBranchName"></param>
    internal static void PullChanges(string? repositoryPath, string? originName, string? pullBranchName)
    {
        using (var repo = new Repository(repositoryPath))
        {
            GeneralGitOperations.PullChanges(repo, originName ?? "", pullBranchName ?? "");
        }
    }

    internal static void GetLocalBranches(string? repositoryPath)
    {
        using (var repo = new Repository(repositoryPath))
        {
            GeneralGitOperations.GetLocalBranches(repo);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repositoryPath"></param>
    /// <param name="pushRefSpec"></param>
    /// <param name="pushOptions"></param>
    internal static void PushChanges(string? repositoryPath, string? pushRefSpec, PushOptions pushOptions)
    {
        using (var repo = new Repository(repositoryPath))
        {
            GeneralGitOperations.PushChanges(repo, pushRefSpec ?? "", pushOptions);
        }
    }

    internal static void CloneRepo(string? repositoryUrl, string? repositoryPath)
    {
        GeneralGitOperations.CloneRepo(repositoryUrl ?? "", repositoryPath ?? "");
    }
}