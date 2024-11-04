using LibGit2Sharp;
using LibGit2Sharp.Core;
using LibGit2Sharp.Handlers;

namespace WorkflowLib.Examples.LibGit2SharpExample.UseCases;

/// <summary>
/// This class includes general and atomic usage scenarios for the LibGit2Sharp library.
/// </summary>
public class GeneralGitOperations
{
    /// <summary>
    /// Gets the list of all remotes.
    /// </summary>
    public static IEnumerable<Remote> GetRemotes(Repository repo)
    {
        return repo.Network.Remotes.ToList();
    }

    /// <summary>
    /// Gets the list of all branches.
    /// </summary>
    public static IEnumerable<Branch> GetBranches(Repository repo)
    {
        return repo.Branches.ToList();
    }

    /// <summary>
    /// Gets the list of local branches.
    /// </summary>
    public static IEnumerable<Branch> GetLocalBranches(Repository repo)
    {
        IEnumerable<Branch> allBranches = GetBranches(repo);
        if (allBranches == null || !allBranches.Any())
        {
            throw new System.Exception($"No branches found for the repository '{repo.Info.Path}'");
        }
        return allBranches.Where(x => !x.IsRemote).ToList();
    }
    
    /// <summary>
    /// Clones remote repository into local folder.
    /// </summary>
    public static string CloneRepo(string url, string pathToRepo)
    {
        return Repository.Clone(url, pathToRepo);
    }
    
    /// <summary>
    /// Switch the currently active branch.
    /// If the branch does not exist, create a new branch.
    /// </summary>
    public static void Checkout(Repository repo, string branchName)
    {
        Branch branchFrom = repo.Head;
        Branch branchTo = repo.Branches[branchName];
        if (branchTo == null)
        {
            branchTo = repo.CreateBranch(branchName);
        }
        branchTo = Commands.Checkout(repo, branchName);
    }
    
    /// <summary>
    /// Pulls changes from the remote repository.
    /// </summary>
    public static void PullChanges(Repository repo, string remoteName, string branchName)
    {
        // 
    }

    /// <summary>
    /// Fetches changes from the remote repository.
    /// </summary>
    public static void FetchChanges(Repository repo, string remoteName)
    {
        string logMessage = string.Empty;
        var remote = repo.Network.Remotes[remoteName];
        if (remote == null)
        {
            throw new System.Exception($"The specified romote '{remoteName}' is not found");
        }
        var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
        Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);
    }

    /// <summary>
    /// Merges changes from the remote repository.
    /// </summary>
    public static MergeResult MergeBranch(Repository repo, string branchName, Signature author)
    {
        Branch branch = repo.Branches[branchName];
        if (branch == null)
        {
            throw new System.Exception($"Branch '{branchName}' could not be found");
        }
        return repo.Merge(branch, author);
    }

    /// <summary>
    /// Fetches and merges changes from the remote repository.
    /// </summary>
    public static void FetchAndMergeChanges(Repository repo, string remoteName, string branchName, Signature author)
    {
        FetchChanges(repo, remoteName);
        MergeBranch(repo, branchName, author);
    }
    
    /// <summary>
    /// Stage specific changes made in the repository.
    /// </summary>
    public static void StageChanges(Repository repo, IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            repo.Index.Add(file);
            repo.Index.Write();
        }
    }

    /// <summary>
    /// Stage all changes made in the repository.
    /// </summary>
    public static void StageAllChanges(Repository repo)
    {
        Commands.Stage(repo, "*");
    }

    /// <summary>
    /// Commit changes made in the repository.
    /// </summary>
    public static void CommitChanges(Repository repo, string message, Signature author, Signature committer)
    {
        Commit commit = repo.Commit(message, author, committer);
    }

    /// <summary>
    /// Push changes to the remote repository.
    /// </summary>
    public static void PushChanges(Repository repo, string pushRefSpec, PushOptions options)
    {
        Remote remote = repo.Network.Remotes["origin"];
        repo.Network.Push(remote, pushRefSpec, options);
    }

    /// <summary>
    /// Get changes made by the last commit.
    /// </summary>
    public static Patch GetLastCommitChanges(Repository repo)
    {
        Tree commitTree = repo.Head.Tip.Tree;
        Tree parentCommitTree = repo.Head.Tip.Parents.First().Tree;

        return repo.Diff.Compare<Patch>(parentCommitTree, commitTree);
    }

    /// <summary>
    /// Compare branch Head and specified branch.
    /// </summary>
    public static Patch CompareBranchWithHead(Repository repo, string branchName)
    {
        Branch branch = repo.Branches[branchName];
        if (branch == null)
        {
            throw new System.Exception($"Specified branch '{branchName}' does not exist");
        }

        Tree commitTreeBranch = branch.Tip.Tree;
        Tree commitTreeHead = repo.Head.Tip.Tree;

        return repo.Diff.Compare<Patch>(commitTreeBranch, commitTreeHead);
    }
}