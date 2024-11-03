using LibGit2Sharp;
using LibGit2Sharp.Core;
using LibGit2Sharp.Handlers;

namespace WorkflowLib.Examples.LibGit2SharpExample.UseCases;

/// <summary>
/// This class includes general and atomic usage scenarios for the LibGit2Sharp library.
/// </summary>
public class GeneralUseCases
{
    /// <summary>
    /// Gets the list of all branches.
    /// </summary>
    public static void GetBranches(string pathToRepo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            var allRemotes = repo.Network.Remotes.ToList();
            foreach (var r in allRemotes)
            {
                System.Console.WriteLine($"remote: {r.Name} \t url: {r.Url}");
            }
            var allBranches = repo.Branches.ToList();
            foreach (var b in allBranches)
            {
                System.Console.WriteLine($"branch: {b.CanonicalName} \t FriendlyName: {b.FriendlyName} \t remote: {b.IsRemote} \t head: {b.IsCurrentRepositoryHead}");
            }
        }
    }

    /// <summary>
    /// Gets the list of local branches.
    /// </summary>
    public static void GetLocalBranches(string pathToRepo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            var allBranches = repo.Branches.Where(x => !x.IsRemote).ToList();
            foreach (var b in allBranches)
            {
                System.Console.WriteLine($"branch: {b.CanonicalName} \t FriendlyName: {b.FriendlyName} \t head: {b.IsCurrentRepositoryHead}");
            }
        }
    }
    
    /// <summary>
    /// Clones remote repository into local folder.
    /// </summary>
    public static void CloneRepo(string url, string pathToRepo)
    {
        string clonedRepoPath = Repository.Clone(url, pathToRepo);
        System.Console.WriteLine($"input: '{pathToRepo}', output: '{clonedRepoPath}'");
    }
    
    /// <summary>
    /// Switch the currently active branch.
    /// If the branch does not exist, create a new branch.
    /// </summary>
    public static void Checkout(string pathToRepo, string branchName)
    {
        using (var repo = new Repository(pathToRepo))
        {
            Branch branchFrom = repo.Head;
            Branch branchTo = repo.Branches[branchName];
            if (branchTo == null)
            {
                branchTo = repo.CreateBranch(branchName);
            }
            branchTo = Commands.Checkout(repo, branchName);
            System.Console.WriteLine($"git checkout {branchFrom.FriendlyName}..{branchTo.FriendlyName}");
        }
    }
    
    /// <summary>
    /// Switch the currently active branch.
    /// If the branch does not exist, create a new branch.
    /// </summary>
    public static void Checkout(string pathToRepo, string branchNameFrom, string branchNameTo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            Branch branchHead = repo.Head;
            Branch branchFrom = branchHead.FriendlyName != branchNameFrom && branchHead.CanonicalName != branchNameFrom
                ? Commands.Checkout(repo, branchNameFrom)
                : branchHead;

            Branch branchTo = repo.Branches[branchNameTo];
            if (branchTo == null)
            {
                branchTo = repo.CreateBranch(branchNameTo);
            }
            branchTo = Commands.Checkout(repo, branchNameTo);
            System.Console.WriteLine($"git checkout {branchFrom.FriendlyName}..{branchTo.FriendlyName}");
        }
    }

    /// <summary>
    /// Pulls changes from the remote repository.
    /// </summary>
    public static void PullChanges(string pathToRepo, string remoteName, string branchName)
    {
        // 
    }

    /// <summary>
    /// Fetches and merges changes from the remote repository.
    /// </summary>
    public static void FetchAndMergeChanges(string pathToRepo, string remoteName, string branchName)
    {
        System.Console.WriteLine($"Imitate git pull");

        string logMessage = "";
        using (var repo = new Repository(pathToRepo))
        {
            // Get the origin remote.
            var remote = repo.Network.Remotes[remoteName];
            if (remote == null)
            {
                throw new System.Exception($"The specified romote '{remoteName}' is not found");
            }
            var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);

            // Fetch changes.
            System.Console.WriteLine($"1. git fetch");
            Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);

            // Get the last commit, its author and message.
            System.Console.WriteLine($"2. Getting the last commit");
            Branch branch = repo.Branches[branchName];
            if (branch == null)
            {
                throw new System.Exception($"Branch '{branchName}' could not be found");
            }
            Commit branchTip = branch.Tip;
            if (branchTip == null)
            {
                throw new System.Exception($"The last commit of the branch '{branchName}' could not be found");
            }
            Signature branchTipAuthor = branchTip.Author;
            if (branchTip == null)
            {
                throw new System.Exception($"The author of the last commit of the branch '{branchName}' could not be found");
            }
            System.Console.WriteLine($"{branchTipAuthor.When} | {branchTip.MessageShort}");

            // Merge changes locally.
            System.Console.WriteLine($"3. git merge");
            MergeResult mergeResult = repo.Merge(branchTip, branchTipAuthor);
            System.Console.WriteLine($"merge status: {mergeResult.Status}");
        }
        Console.WriteLine(logMessage);
    }
    
    /// <summary>
    /// Stage specific changes made in the repository.
    /// </summary>
    public static void StageChanges(string pathToRepo, IEnumerable<string> files)
    {
        using (var repo = new Repository(pathToRepo))
        {
            foreach (var file in files)
            {
                repo.Index.Add(file);
                repo.Index.Write();
            }
        }
    }

    /// <summary>
    /// Stage all changes made in the repository.
    /// </summary>
    public static void StageAllChanges(string pathToRepo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            Commands.Stage(repo, "*");
        }
    }

    /// <summary>
    /// Commite changes made in the repository.
    /// </summary>
    public static void CommitChanges(string pathToRepo, string message, Signature author, Signature committer)
    {
        using (var repo = new Repository(pathToRepo))
        {
            Commit commit = repo.Commit(message, author, committer);
        }
    }

    /// <summary>
    /// Push changes to the remote repository.
    /// </summary>
    public static void PushChanges(string pathToRepo, string pushRefSpec, PushOptions options)
    {
        using (var repo = new Repository(pathToRepo))
        {
            Remote remote = repo.Network.Remotes["origin"];
            repo.Network.Push(remote, pushRefSpec, options);
        }
    }

    /// <summary>
    /// Get changes made by the last commit.
    /// </summary>
    public static void GetLastCommitChanges(string pathToRepo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            Tree commitTree = repo.Head.Tip.Tree;
            Tree parentCommitTree = repo.Head.Tip.Parents.First().Tree;

            var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree);

            foreach (var ptc in patch)
            {
                Console.WriteLine(ptc.Status + " : " + ptc.Path);
            }
        }
    }

    /// <summary>
    /// Compare branch Head and specified branch.
    /// </summary>
    public static void CompareBranchWithHead(string pathToRepo, string branchName)
    {
        using (var repo = new Repository(pathToRepo))
        {
            Branch branch = repo.Branches[branchName];
            if (branch == null)
            {
                throw new System.Exception($"Specified branch '{branchName}' does not exist");
            }

            Tree commitTreeBranch = branch.Tip.Tree;
            Tree commitTreeHead = repo.Head.Tip.Tree;

            var patch = repo.Diff.Compare<Patch>(commitTreeBranch, commitTreeHead);

            foreach (var ptc in patch)
            {
                Console.WriteLine(ptc.Status + " : " + ptc.Path);
            }
        }
    }
}